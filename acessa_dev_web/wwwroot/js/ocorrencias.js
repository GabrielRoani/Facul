// Busca em tempo real para ocorrências
function setupOcorrenciasSearch() {
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

// Validação de formulário de ocorrências
function validateOcorrenciaForm() {
    const form = document.getElementById('ocorrenciaForm');
    if (form) {
        form.addEventListener('submit', function (e) {
            const requiredFields = form.querySelectorAll('[required]');
            let isValid = true;

            requiredFields.forEach(field => {
                if (!field.value.trim()) {
                    isValid = false;
                    field.style.borderColor = 'var(--cor-erro)';
                } else {
                    field.style.borderColor = '';
                }
            });

            if (!isValid) {
                e.preventDefault();
                showNotification('Preencha todos os campos obrigatórios', 'error');
            }
        });
    }
}

// Inicialização
document.addEventListener('DOMContentLoaded', function () {
    setupOcorrenciasSearch();
    validateOcorrenciaForm();

    // Definir data atual como padrão nos forms de criação
    const dataInput = document.getElementById('Data');
    if (dataInput && !dataInput.value && window.location.pathname.includes('/Create')) {
        const now = new Date();
        dataInput.value = now.toISOString().slice(0, 16);
    }
});

// Função global de notificação
function showNotification(message, type = 'success') {
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.textContent = message;
    document.body.appendChild(notification);

    setTimeout(() => {
        if (notification.parentElement) {
            notification.remove();
        }
    }, 5000);
}