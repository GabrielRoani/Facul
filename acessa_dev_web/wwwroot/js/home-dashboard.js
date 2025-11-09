document.addEventListener('DOMContentLoaded', function () {

    const userMenuBtn = document.getElementById('userMenuBtn');
    const userDropdown = document.getElementById('userDropdown');

    if (userMenuBtn && userDropdown) {
        userMenuBtn.addEventListener('click', function (event) {
            userDropdown.classList.toggle('show');
            event.stopPropagation();
        });

        window.addEventListener('click', function (event) {
            if (!userMenuBtn.contains(event.target)) {
                userDropdown.classList.remove('show');
            }
        });
    }


    const categoryChips = document.querySelectorAll('.category-chip');

    categoryChips.forEach(chip => {
        chip.addEventListener('click', function () {
            categoryChips.forEach(c => c.classList.remove('active'));
            this.classList.add('active');
        });
    });


    const clearFiltersBtn = document.querySelector('.sidebar-section .btn-secondary');

    if (clearFiltersBtn) {
        clearFiltersBtn.addEventListener('click', function () {
            const typeSelect = document.querySelector('.filter-select');
            if (typeSelect) {
                typeSelect.selectedIndex = 0;
            }

            const severityCheckboxes = document.querySelectorAll('.filter-group .checkbox-item input[type="checkbox"]');
            severityCheckboxes.forEach(checkbox => {
                checkbox.checked = true;
            });

            categoryChips.forEach((chip, index) => {
                if (index === 0) {
                    chip.classList.add('active');
                } else {
                    chip.classList.remove('active');
                }
            });
        });
    }


    const fab = document.querySelector('.fab');

    if (fab) {
        fab.addEventListener('click', function () {
            alert('Função de "Nova Ocorrência Rápida" a ser implementada.');

            const newOccurrenceForm = document.getElementById('formOcorrencia');
            if (newOccurrenceForm) {
                newOccurrenceForm.scrollIntoView({ behavior: 'smooth' });
            }
        });
    }

    if (document.getElementById('mapDashboard')) {

        const mapDashboard = new ol.Map({
            target: "mapDashboard",
            layers: [
                new ol.layer.Tile({
                    source: new ol.source.OSM()
                })
            ],
            view: new ol.View({
                center: ol.proj.fromLonLat([-46.6333, -23.5505]),
                zoom: 13
            })
        });
    };
});