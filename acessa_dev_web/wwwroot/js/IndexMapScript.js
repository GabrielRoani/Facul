// Função para abrir mapa completo
document.getElementById("abrirMapa").addEventListener("click", () => {
    window.location.href = "/Home/Maps";
});

// Inicializa o mini mapa
const mapMini = new ol.Map({
    target: "mapMini",
    layers: [new ol.layer.Tile({ source: new ol.source.OSM() })],
    view: new ol.View({
        center: ol.proj.fromLonLat([-46.6333, -23.5505]), // São Paulo
        zoom: 13
    })
});

// Categorias exibidas com ícones atualizados e coerentes
const categoriasMini = {
    bar: {
        icon: "https://cdn-icons-png.flaticon.com/512/1046/1046784.png", // 🍽️ prato e talheres
        filtro: '["amenity"~"bar|cafe|restaurant"]'
    },
    school: {
        icon: "https://cdn-icons-png.flaticon.com/512/3135/3135755.png", // 🎓 escola
        filtro: '["amenity"~"school|university|library"]'
    },
    hospital: {
        icon: "https://cdn-icons-png.flaticon.com/512/2967/2967350.png", // ⛑️ cruz médica
        filtro: '["amenity"~"hospital|clinic|pharmacy"]'
    }
};

// Criação das camadas
const layersMini = {};
for (const key in categoriasMini) {
    const src = new ol.source.Vector();
    const layer = new ol.layer.Vector({ source: src });
    mapMini.addLayer(layer);
    layersMini[key] = { source: src, layer };
}
// Função para alternar visibilidade das camadas pelos checkboxes
document.getElementById("chkBar").addEventListener("change", e => {
    layersMini.bar.layer.setVisible(e.target.checked);
});
document.getElementById("chkSchool").addEventListener("change", e => {
    layersMini.school.layer.setVisible(e.target.checked);
});
document.getElementById("chkHospital").addEventListener("change", e => {
    layersMini.hospital.layer.setVisible(e.target.checked);
});

// Inicialmente todas visíveis
layersMini.bar.layer.setVisible(true);
layersMini.school.layer.setVisible(true);
layersMini.hospital.layer.setVisible(true);


// Função para criar marcadores
function criarMarcador(lon, lat, iconUrl) {
    const f = new ol.Feature({
        geometry: new ol.geom.Point(ol.proj.fromLonLat([lon, lat]))
    });
    f.setStyle(new ol.style.Style({
        image: new ol.style.Icon({ src: iconUrl, scale: 0.05 }) // ícones menores e uniformes
    }));
    return f;
}

// Carregar POIs reais do OpenStreetMap
async function carregarPOIsMini() {
    const extent = mapMini.getView().calculateExtent(mapMini.getSize());
    const bottomLeft = ol.proj.toLonLat(ol.extent.getBottomLeft(extent));
    const topRight = ol.proj.toLonLat(ol.extent.getTopRight(extent));
    const bbox = `${bottomLeft[1]},${bottomLeft[0]},${topRight[1]},${topRight[0]}`;

    if (mapMini.getView().getZoom() < 12) return; // só carrega quando aproximado

    for (const key in categoriasMini) {
        const cat = categoriasMini[key];
        const query = `[out:json][timeout:25];(node${cat.filtro}(${bbox}););out body;`;
        const url = "https://overpass.kumi.systems/api/interpreter?data=" + encodeURIComponent(query);

        try {
            const res = await fetch(url);
            const data = await res.json();
            layersMini[key].source.clear();

            data.elements.forEach(el => {
                if (el.type === "node" && el.lon && el.lat) {
                    const feat = criarMarcador(el.lon, el.lat, cat.icon);
                    layersMini[key].source.addFeature(feat);
                }
            });
        } catch (err) {
            console.error("Erro ao carregar POIs miniatura", key, err);
        }
    }
}

// --- Atualiza ao mover o mapa ---
map.on('moveend', carregarPOIs);

// Carrega automaticamente ao abrir
carregarPOIs();

// === POPUP MODERNO ===
document.addEventListener("DOMContentLoaded", () => {

    // Criar popup HTML
    const popup = document.createElement("div");
    popup.id = "map-popup";
    popup.innerHTML = `
        <div class="popup-content">
            <h3 id="popup-titulo"></h3>
            <div class="popup-botoes">
                <button id="btn-avaliar">💬 Avaliar</button>
                <button id="btn-ocorrencia">🚨 Ocorrência</button>
            </div>
            <button id="popup-fechar">✖</button>
        </div>
    `;
    document.body.appendChild(popup);

    const popupTitulo = document.getElementById("popup-titulo");
    const btnAvaliar = document.getElementById("btn-avaliar");
    const btnOcorrencia = document.getElementById("btn-ocorrencia");
    const popupFechar = document.getElementById("popup-fechar");

    // Funções de abrir/fechar
    function mostrarPopup(nome, tipo) {
        popupTitulo.textContent = `${nome} (${categorias[tipo]?.label || tipo})`;
        popup.classList.add("ativo");
    }

    function fecharPopup() {
        popup.classList.remove("ativo");
    }

    popupFechar.addEventListener("click", fecharPopup);
    popup.addEventListener("click", (e) => {
        if (e.target === popup) fecharPopup();
    });

    btnAvaliar.addEventListener("click", () => {
        window.location.href = "/Avaliacoes/Index";
    });

    btnOcorrencia.addEventListener("click", () => {
        window.location.href = "/Ocorrencias/Index";
    });

    // ✅ Clique no mapa (sem alert)
    map.on("singleclick", (evt) => {
        let achou = false;
        map.forEachFeatureAtPixel(evt.pixel, (f) => {
            const nome = f.get("nome");
            const tipo = f.get("tipo");
            mostrarPopup(nome, tipo);
            achou = true;
        });
        if (!achou) fecharPopup();
    });
});



});
