// ===== Smooth Scroll para Links de Navegação =====
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        }
    });
});

// ===== Animação de Scroll para Elementos =====
const observerOptions = {
    threshold: 0.1,
    rootMargin: '0px 0px -50px 0px'
};

const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.style.opacity = '1';
            entry.target.style.transform = 'translateY(0)';
        }
    });
}, observerOptions);

// Aplicar animação aos cards e seções
document.querySelectorAll('.step-card, .category-card, .feature-item').forEach(el => {
    el.style.opacity = '0';
    el.style.transform = 'translateY(30px)';
    el.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
    observer.observe(el);
});

// ===== Header Scroll Effect =====
let lastScroll = 0;
const header = document.querySelector('.header');

window.addEventListener('scroll', () => {
    const currentScroll = window.pageYOffset;

    if (currentScroll > 100) {
        header.style.boxShadow = '0 4px 16px rgba(0, 0, 0, 0.12)';
    } else {
        header.style.boxShadow = '0 2px 8px rgba(0, 0, 0, 0.08)';
    }

    lastScroll = currentScroll;
});

// ===== Contador Animado para Estatísticas =====
function animateCounter(element, target, duration = 2000) {
    let start = 0;
    const increment = target / (duration / 16);

    const timer = setInterval(() => {
        start += increment;
        if (start >= target) {
            element.textContent = target.toLocaleString('pt-BR');
            clearInterval(timer);
        } else {
            element.textContent = Math.floor(start).toLocaleString('pt-BR');
        }
    }, 16);
}

// Observar quando as estatísticas entram na viewport
const statsObserver = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            const statNumbers = entry.target.querySelectorAll('.stat-number');
            statNumbers.forEach(stat => {
                const target = parseInt(stat.textContent.replace(/\./g, ''));
                animateCounter(stat, target);
            });
            statsObserver.unobserve(entry.target);
        }
    });
}, { threshold: 0.5 });

const heroStats = document.querySelector('.hero-stats');
if (heroStats) {
    statsObserver.observe(heroStats);
}

// ===== Modal de Reportar Ocorrência (Simulação) =====
const reportarBtn = document.getElementById('reportarBtn');

if (reportarBtn) {
    reportarBtn.addEventListener('click', () => {
        alert('Funcionalidade de reportar ocorrência será implementada na versão completa da aplicação.\n\nVocê será direcionado para um formulário onde poderá:\n• Adicionar fotos/vídeos\n• Informar localização\n• Descrever a ocorrência\n• Categorizar o problema');
    });
}

// ===== Interação com Mapa (Simulação) =====
const mapPlaceholder = document.getElementById('mapPlaceholder');

if (mapPlaceholder) {
    mapPlaceholder.addEventListener('click', () => {
        console.log('[v0] Mapa clicado - Aqui seria carregado o mapa interativo completo');
        alert('O mapa interativo completo será carregado aqui, permitindo:\n• Visualizar todas as ocorrências\n• Filtrar por categoria\n• Ver avaliações de locais\n• Adicionar novas ocorrências diretamente no mapa');
    });
}

// ===== Highlight do Link Ativo na Navegação =====
const sections = document.querySelectorAll('section[id]');
const navLinks = document.querySelectorAll('.nav-link');

window.addEventListener('scroll', () => {
    let current = '';

    sections.forEach(section => {
        const sectionTop = section.offsetTop;
        const sectionHeight = section.clientHeight;
        if (pageYOffset >= sectionTop - 200) {
            current = section.getAttribute('id');
        }
    });

    navLinks.forEach(link => {
        link.style.color = '';
        link.style.backgroundColor = '';
        if (link.getAttribute('href') === `#${current}`) {
            link.style.color = 'var(--cor-primaria)';
            link.style.backgroundColor = 'var(--fundo-secundario)';
        }
    });
});

// ===== Animação de Hover nos Cards de Categoria =====
const categoryCards = document.querySelectorAll('.category-card');

categoryCards.forEach(card => {
    card.addEventListener('mouseenter', function () {
        this.style.borderColor = 'var(--cor-primaria)';
    });

    card.addEventListener('mouseleave', function () {
        this.style.borderColor = 'transparent';
    });
});

// ===== Log de Debug =====
console.log('[v0] Acessa+ Homepage carregada com sucesso');
console.log('[v0] Paleta de cores aplicada:', {
    primaria: getComputedStyle(document.documentElement).getPropertyValue('--cor-primaria'),
    sucesso: getComputedStyle(document.documentElement).getPropertyValue('--cor-sucesso'),
    alerta: getComputedStyle(document.documentElement).getPropertyValue('--cor-alerta'),
    erro: getComputedStyle(document.documentElement).getPropertyValue('--cor-erro')
});