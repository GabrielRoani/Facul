const map = new ol.Map({
    target: 'mapFull',
    layers: [new ol.layer.Tile({ source: new ol.source.OSM() })],
    view: new ol.View({
        center: ol.proj.fromLonLat([-46.6333, -23.5505]),
        zoom: 14
    })
});

// Categorias configuradas (ícones padronizados e coerentes)
const categorias = {
    bar: {
        label: "Bares / Cafés / Restaurantes",
        filtro: '["amenity"~"bar|cafe|restaurant"]',
        icon: "https://cdn-icons-png.flaticon.com/512/1046/1046784.png" // 🍽️ prato e talheres
    },
    school: {
        label: "Escolas / Universidades",
        filtro: '["amenity"~"school|university|library"]',
        icon: "https://cdn-icons-png.flaticon.com/512/3135/3135755.png" // 🎓 escola
    },
    hospital: {
        label: "Hospitais / Clínicas / Farmácias",
        filtro: '["amenity"~"hospital|clinic|pharmacy"]',
        icon: "https://cdn-icons-png.flaticon.com/512/2967/2967350.png" // ⛑️ cruz médica
    },
    shop: {
        label: "Lojas / Supermercados",
        filtro: '["shop"]',
        icon: "https://cdn-icons-png.flaticon.com/512/3081/3081559.png" // 🛒 carrinho de compras
    },
    transport: {
        label: "Transporte / Estacionamento",
        filtro: '["amenity"~"bus_station|parking|taxi"]',
        icon: "https://cdn-icons-png.flaticon.com/512/3103/3103446.png" // 🚌 transporte
    }
};

// Camadas para cada categoria
const layers = {};
for (const key in categorias) {
    const src = new ol.source.Vector();
    const layer = new ol.layer.Vector({ source: src });
    map.addLayer(layer);
    layers[key] = { source: src, layer, visible: true };
}

// --- Painel de filtros ---
const filtrosDiv = document.getElementById('filtros');
if (filtrosDiv) {
    for (const key in categorias) {
        const cat = categorias[key];
        const label = document.createElement('label');
        // Começa desmarcado (checkbox vazio)
        label.innerHTML = `<input type="checkbox" data-cat="${key}"><img src="${cat.icon}" width="18"> ${cat.label}`;
        filtrosDiv.appendChild(label);
    }

    filtrosDiv.querySelectorAll('input[type="checkbox"]').forEach(cb => {
        cb.addEventListener('change', e => {
            const cat = e.target.getAttribute('data-cat');
            const visible = e.target.checked;
            layers[cat].layer.setVisible(visible);
            if (visible) carregarPOIs(); // recarrega só quando ativar
        });
    });
}

// --- Função para criar marcador ---
function criarMarcador(lon, lat, nome, tipo, iconUrl) {
    const f = new ol.Feature({
        geometry: new ol.geom.Point(ol.proj.fromLonLat([lon, lat])),
        nome, tipo
    });
    f.setStyle(new ol.style.Style({
        image: new ol.style.Icon({ src: iconUrl, scale: 0.06 })
    }));
    return f;
}

// --- Carrega pontos reais do OpenStreetMap ---
async function carregarPOIs() {
    const extent = map.getView().calculateExtent(map.getSize());
    const bottomLeft = ol.proj.toLonLat(ol.extent.getBottomLeft(extent));
    const topRight = ol.proj.toLonLat(ol.extent.getTopRight(extent));
    const south = bottomLeft[1];
    const west = bottomLeft[0];
    const north = topRight[1];
    const east = topRight[0];
    const bbox = `${south},${west},${north},${east}`;

    if (map.getView().getZoom() < 13) {
        console.warn("🔎 Zoom muito distante. Aproxime para carregar estabelecimentos reais.");
        return;
    }

    for (const key in categorias) {
        const cb = document.querySelector(`input[data-cat="${key}"]`);
        if (!cb || !cb.checked) continue; // só busca categorias ativas

        const cat = categorias[key];
        const query = `[out:json][timeout:25];
            (
                node${cat.filtro}(${bbox});
            );
            out body;`;
        const url = "https://overpass.kumi.systems/api/interpreter?data=" + encodeURIComponent(query);

        try {
            const res = await fetch(url);
            const data = await res.json();
            layers[key].source.clear();

            data.elements.forEach(el => {
                if (el.type === "node" && el.lon && el.lat) {
                    const nome = el.tags.name || "(sem nome)";
                    const feat = criarMarcador(el.lon, el.lat, nome, key, cat.icon);
                    layers[key].source.addFeature(feat);
                }
            });
            console.log(`✅ ${data.elements.length} pontos carregados para ${cat.label}`);
        } catch (err) {
            console.error("❌ Erro ao buscar categoria", key, err);
        }
    }
}

// --- Busca por nome de local (cidade ou estabelecimento) ---
const campoBusca = document.getElementById('filtroTexto');
if (campoBusca) {
    campoBusca.addEventListener('keypress', async e => {
        if (e.key === 'Enter') {
            const termo = campoBusca.value.trim();
            if (!termo) return;

            try {
                const url = `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(termo)}&limit=1`;
                const res = await fetch(url);
                const results = await res.json();

                if (results.length > 0) {
                    const r = results[0];
                    const lon = parseFloat(r.lon);
                    const lat = parseFloat(r.lat);
                    map.getView().setCenter(ol.proj.fromLonLat([lon, lat]));
                    map.getView().setZoom(15);
                    console.log(`📍 Movido para ${r.display_name}`);
                    carregarPOIs();
                } else {
                    alert("❌ Local não encontrado. Tente outro nome.");
                }
            } catch (err) {
                console.error("Erro ao buscar local:", err);
            }
        }
    });
}

// --- Atualiza ao mover o mapa ---
map.on('moveend', carregarPOIs);


// Recarrega ao mover o mapa
map.on('moveend', carregarPOIs);

// Carrega automaticamente
carregarPOIs();

// Popup simples ao clicar
map.on('singleclick', evt => {
    map.forEachFeatureAtPixel(evt.pixel, f => {
        const nome = f.get('nome');
        const tipo = f.get('tipo');
        alert(`📍 ${nome}\nCategoria: ${categorias[tipo]?.label || tipo}`);
    });
});
