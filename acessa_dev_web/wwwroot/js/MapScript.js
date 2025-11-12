// ========== MAPA PRINCIPAL ==========
let map;
let layerPontos;

document.addEventListener("DOMContentLoaded", async () => {
    inicializarMapa();
});

async function inicializarMapa() {
    map = new ol.Map({
        target: 'mapFull',
        layers: [
            new ol.layer.Tile({
                source: new ol.source.OSM()
            })
        ],
        view: new ol.View({
            center: ol.proj.fromLonLat([-46.6333, -23.5505]), // São Paulo
            zoom: 12
        })
    });

    await carregarPontos("restaurant");
}

// ========== FUNÇÃO DE BUSCA ==========
async function carregarPontos(categoria) {
    const url = `
        https://overpass-api.de/api/interpreter?data=
        [out:json];
        node["amenity"="${categoria}"](around:10000,-23.5505,-46.6333);
        out;
    `.trim();

    try {
        const response = await fetch(url);
        const data = await response.json();

        if (layerPontos) map.removeLayer(layerPontos);

        const features = data.elements.map(el => {
            const nome = el.tags.name || "Sem nome";
            const ponto = new ol.Feature({
                geometry: new ol.geom.Point(ol.proj.fromLonLat([el.lon, el.lat])),
                nome: nome,
                categoria: categoria
            });
            return ponto;
        });

        const vetorFonte = new ol.source.Vector({ features });
        const icone = new ol.style.Style({
            image: new ol.style.Icon({
                anchor: [0.5, 1],
                src: "/images/marker.png"
            })
        });

        layerPontos = new ol.layer.Vector({
            source: vetorFonte,
            style: icone
        });

        map.addLayer(layerPontos);

        // Clique no ponto
        map.on('click', function (evt) {
            map.forEachFeatureAtPixel(evt.pixel, function (feature) {
                const nome = feature.get("nome");
                const categoria = feature.get("categoria");
                mostrarPopupPersonalizado(nome, categoria);
            });
        });

        console.log(`${features.length} pontos carregados para ${categoria}`);
    } catch (erro) {
        console.error("Erro ao buscar categoria", categoria, erro);
    }
}

// ========== POPUP PERSONALIZADO ==========
function mostrarPopupPersonalizado(nome, categoria) {
    const popup = document.getElementById("map-popup");
    if (!popup) return;

    document.getElementById("popup-titulo").textContent = nome;
    document.getElementById("popup-descricao").textContent = `Categoria: ${categoria}`;
    popup.classList.add("ativo");
}

// ========== CONTROLE DO POPUP ==========
document.addEventListener("DOMContentLoaded", () => {
    const popup = document.getElementById("map-popup");
    const fecharPopup = document.getElementById("popup-fechar");
    const btnAvaliar = document.getElementById("btn-avaliar");
    const btnOcorrencia = document.getElementById("btn-ocorrencia");

    if (popup && fecharPopup) {
        fecharPopup.addEventListener("click", () => popup.classList.remove("ativo"));
        popup.addEventListener("click", (e) => {
            if (e.target === popup) popup.classList.remove("ativo");
        });
    }

    if (btnAvaliar) {
        btnAvaliar.addEventListener("click", () => {
            window.location.href = "/valiacoes/Index";
        });
    }

    if (btnOcorrencia) {
        btnOcorrencia.addEventListener("click", () => {
            window.location.href = "/Ocorrencias/Index";
        });
    }
});
