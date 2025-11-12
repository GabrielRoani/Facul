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

    // Adicionar preenchimento automático dos campos do local
    if (window.locaisData) {
        setupLocalAutoFill(window.locaisData);
    }

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

function setupLocalAutoFill(locais) {
    const selectLocal = document.querySelector('select[name="idLocal"]');
    const nomeInput = document.querySelector('input[name="Nome"]');
    const enderecoInput = document.querySelector('input[name="Endereco"]');
    const latitudeInput = document.querySelector('input[name="Latitude"]');
    const longitudeInput = document.querySelector('input[name="Longitude"]');

    if (!selectLocal) return;

    selectLocal.addEventListener('change', function () {
        const selectedId = parseInt(this.value);
        const local = locais.find(l => l.idLocal === selectedId);

        nomeInput.readOnly = true;
        enderecoInput.readOnly = true;
        latitudeInput.readOnly = true;
        longitudeInput.readOnly = true;

        if (local) {
            nomeInput.value = local.Nome;
            enderecoInput.value = local.Endereco;
            latitudeInput.value = local.Latitude;
            longitudeInput.value = local.Longitude;
        } else {
            nomeInput.value = '';
            enderecoInput.value = '';
            latitudeInput.value = '';
            longitudeInput.value = '';
        }
    });
}