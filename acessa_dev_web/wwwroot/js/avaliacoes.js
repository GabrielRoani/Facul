// Sistema de Estrelas para Avaliações
function setupRatingStars() {
    const stars = document.querySelectorAll('.star');
    const notaInput = document.getElementById('ValorAvaliacao') || document.getElementById('valorAvaliacao');

    if (!stars.length || !notaInput) return;

    stars.forEach(star => {
        star.addEventListener('click', function () {
            const value = parseInt(this.getAttribute('data-value'));
            notaInput.value = value;

            // Atualizar visual das estrelas
            stars.forEach((s, index) => {
                if (index < value) {
                    s.classList.add('active');
                } else {
                    s.classList.remove('active');
                }
            });
        });
    });

    // Inicializar com valor atual se existir
    if (notaInput.value) {
        const value = parseInt(notaInput.value);
        stars.forEach((star, index) => {
            if (index < value) {
                star.classList.add('active');
            }
        });
    }
}

// Busca em tempo real para avaliações
function setupAvaliacoesSearch() {
    const searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.addEventListener('input', function () {
            const searchTerm = this.value.toLowerCase();
            const rows = document.querySelectorAll('.data-table tbody tr');

            rows.forEach(row => {
                const text = row.textContent.toLowerCase();
                if (text.includes(searchTerm)) {
                    row.style.display = '';
                } else {
                    row.style.display = 'none';
                }
            });
        });
    }
}

// Inicialização
document.addEventListener('DOMContentLoaded', function () {
    setupRatingStars();
    setupAvaliacoesSearch();

    // Definir data atual como padrão nos forms de criação
    const dataInput = document.getElementById('Data');
    if (dataInput && !dataInput.value && window.location.pathname.includes('/Create')) {
        const now = new Date();
        dataInput.value = now.toISOString().slice(0, 16);
    }
});