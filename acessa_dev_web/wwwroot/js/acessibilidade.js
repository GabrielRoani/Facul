// ======== MENU DE ACESSIBILIDADE ========
document.addEventListener("DOMContentLoaded", () => {
    const toggleBtn = document.getElementById("toggleAcessibilidade");
    const opcoes = document.getElementById("acessibilidade-opcoes");

    if (toggleBtn && opcoes) {
        // 👉 Alterna o menu ao clicar no botão
        toggleBtn.addEventListener("click", (e) => {
            e.stopPropagation(); // impede que o clique feche imediatamente
            const expanded = toggleBtn.getAttribute("aria-expanded") === "true";
            toggleBtn.setAttribute("aria-expanded", !expanded);
            opcoes.hidden = expanded;
        });

        // 👉 Fecha o menu ao clicar fora dele
        document.addEventListener("click", (e) => {
            if (
                !opcoes.hidden && // só tenta fechar se estiver aberto
                !opcoes.contains(e.target) &&
                !toggleBtn.contains(e.target)
            ) {
                opcoes.hidden = true;
                toggleBtn.setAttribute("aria-expanded", "false");
            }
        });
    }

    // 🔄 Restaura preferências e atualiza botões
    restaurarPreferencias();
    atualizarBotaoSom();
});


// ======== VARIÁVEIS GLOBAIS ========
let somAtivo = false;
let leituraPorHoverAtiva = false;
let elementosMonitorados = [];
let lendo = false;
let utteranceAtual = null;

const audioClick = new Audio("https://actions.google.com/sounds/v1/tools/18v_cordless_drill_switch.ogg?hl=pt-br");
audioClick.preload = "auto";
audioClick.volume = 0.6;

// ======== FUNÇÕES GERAIS ========
function salvarPreferencias(configExtra = {}) {
    const configAtual = JSON.parse(localStorage.getItem("acessibilidade")) || {};
    const novaConfig = { ...configAtual, ...configExtra };
    localStorage.setItem("acessibilidade", JSON.stringify(novaConfig));
}

function restaurarPreferencias() {
    const config = JSON.parse(localStorage.getItem("acessibilidade") || "{}");

    if (config.fonte) document.documentElement.style.fontSize = config.fonte + "px";
    if (config.contraste) document.body.classList.add("alto-contraste");
    if (config.leitura) document.body.classList.add("modo-leitura");
    if (config.noturno) document.body.classList.add("modo-noturno");
    if (config.daltonismo) document.body.classList.add(`daltonismo-${config.daltonismo}`);
    somAtivo = config.som ?? false;

    // restaura modo leitura por hover se estava ativo
    if (config.leituraPorHover) {
        leituraPorHoverAtiva = true;
        ativarLeituraPorHover(true);
    }
}

// ======== TAMANHO DA FONTE ========
function alterarFonte(fator) {
    const html = document.documentElement;
    const estiloAtual = parseFloat(window.getComputedStyle(html).fontSize);
    const novoTamanho = estiloAtual * fator;
    html.style.fontSize = novoTamanho + "px";
    salvarPreferencias({ fonte: novoTamanho });
}

// ======== ALTO CONTRASTE ========
function alternarContraste() {
    document.body.classList.toggle("alto-contraste");
    salvarPreferencias({ contraste: document.body.classList.contains("alto-contraste") });
}

// ======== MODO LEITURA ========
function modoLeitura() {
    document.body.classList.toggle("modo-leitura");
    salvarPreferencias({ leitura: document.body.classList.contains("modo-leitura") });
}

// ======== MODO NOTURNO ========
function modoNoturno() {
    document.body.classList.toggle("modo-noturno");
    salvarPreferencias({ noturno: document.body.classList.contains("modo-noturno") });
}

// ======== MODO DALTONISMO ========
function modoDaltonismo(tipo) {
    document.body.classList.remove("daltonismo-protanopia", "daltonismo-deuteranopia", "daltonismo-tritanopia");
    if (tipo) {
        document.body.classList.add(`daltonismo-${tipo}`);
        salvarPreferencias({ daltonismo: tipo });
    } else {
        salvarPreferencias({ daltonismo: "" });
    }
}

// ======== SOM RELAXANTE GLOBAL ========
function alternarSom() {
    somAtivo = !somAtivo;
    salvarPreferencias({ som: somAtivo });
    atualizarBotaoSom();
}

function atualizarBotaoSom() {
    const btn = document.getElementById("toggleSom");
    if (btn) btn.textContent = somAtivo ? "🔊 Som" : "🔇 Som";
}

document.addEventListener("click", (e) => {
    const elemento = e.target.closest("button");
    if (somAtivo && elemento) {
        audioClick.currentTime = 0;
        audioClick.play().catch(() => { });
    }
});

// ======== LEITURA POR VOZ ========
function falarTexto(texto) {
    if (!('speechSynthesis' in window)) return;
    if (lendo) window.speechSynthesis.cancel();
    if (!texto || texto.trim().length < 2) return;

    utteranceAtual = new SpeechSynthesisUtterance(texto.trim());
    utteranceAtual.lang = 'pt-BR';
    utteranceAtual.rate = 1;
    utteranceAtual.pitch = 1;
    utteranceAtual.volume = 1;

    const vozes = window.speechSynthesis.getVoices();
    const vozBR = vozes.find(v => v.lang.startsWith('pt'));
    if (vozBR) utteranceAtual.voice = vozBR;

    window.speechSynthesis.speak(utteranceAtual);
    lendo = true;

    utteranceAtual.onend = () => {
        lendo = false;
    };
}

function handleHoverLeitura(e) {
    const el = e.target;
    if (!el || !el.innerText || el.innerText.trim().length < 3) return;
    el.classList.add('lendo');
    setTimeout(() => el.classList.remove('lendo'), 2000);
    falarTexto(el.innerText);
}

function ativarLeituraPorHover(forcarAtivar = false) {
    const seletor = 'p, a, button, h1, h2, h3, h4, h5, h6, li, span';
    elementosMonitorados = Array.from(document.querySelectorAll(seletor));

    // alterna estado
    leituraPorHoverAtiva = forcarAtivar ? true : !leituraPorHoverAtiva;

    if (leituraPorHoverAtiva) {
        elementosMonitorados.forEach(el => el.addEventListener('mouseenter', handleHoverLeitura));
        salvarPreferencias({ leituraPorHover: true });
        alert("🔊 Leitura automática por hover ativada!");
    } else {
        window.speechSynthesis.cancel();
        elementosMonitorados.forEach(el => el.removeEventListener('mouseenter', handleHoverLeitura));
        salvarPreferencias({ leituraPorHover: false });
        alert("⏹️ Leitura automática por hover desativada.");
    }
}

// ======== RESETAR ========
function resetarAcessibilidade() {
    document.body.classList.remove(
        "alto-contraste",
        "modo-leitura",
        "modo-noturno",
        "daltonismo-protanopia",
        "daltonismo-deuteranopia",
        "daltonismo-tritanopia"
    );
    document.documentElement.style.fontSize = "";
    somAtivo = false;
    leituraPorHoverAtiva = false;
    window.speechSynthesis.cancel();
    localStorage.removeItem("acessibilidade");
    atualizarBotaoSom();
}
// Reconhecimento de voz consolidado, persistente e tolerante a erros
let recognition = null;
let vozAtiva = false;

// Cria nova instância do SpeechRecognition
function criarRecognition() {
    const SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
    if (!SpeechRecognition) return null;

    const r = new SpeechRecognition();
    r.lang = 'pt-BR';
    r.continuous = true;
    r.interimResults = false;

    r.onresult = (event) => {
        const comando = event.results[event.results.length - 1][0].transcript.toLowerCase().trim();
        console.log('🗣️ comando:', comando);
        interpretarComando(comando);
    };

    r.onerror = (e) => {
        console.warn('Erro recognition:', e.error);
        // se for erro de not-allowed (permissão), desativa e avisa visualmente
        if (e.error === 'not-allowed' || e.error === 'service-not-allowed') {
            atualizarIndicador(false);
            localStorage.removeItem('vozAtiva');
            vozAtiva = false;
            // opcional: alert("Permissão de microfone negada.");
        }
    };

    r.onend = () => {
        // se a flag ainda estiver ativa, tentamos reiniciar (tolerância a timeouts)
        if (vozAtiva) {
            try {
                r.start();
                console.log('Reconhecimento reiniciado automaticamente...');
            } catch (err) {
                console.warn('Não foi possível reiniciar automaticamente:', err);
            }
        }
    };

    return r;
}

// Inicia reconhecimento (cria instância nova se necessário)
async function iniciarReconhecimento() {
    if (!('SpeechRecognition' in window) && !('webkitSpeechRecognition' in window)) {
        alert('Seu navegador não suporta reconhecimento de voz (use Chrome/Edge).');
        return false;
    }

    // se já existe, evita recriar (mas garante que está rodando)
    if (!recognition) recognition = criarRecognition();
    if (!recognition) return false;

    try {
        recognition.start();
        vozAtiva = true;
        localStorage.setItem('vozAtiva', 'true');
        atualizarIndicador(true);
        console.log('🎤 reconhecimento iniciado');
        return true;
    } catch (err) {
        console.warn('Erro ao iniciar reconhecimento:', err);
        // pode acontecer por falta de gesto do usuário - handle sem crash
        vozAtiva = false;
        atualizarIndicador(false);
        return false;
    }
}

function pararReconhecimento() {
    if (recognition) {
        try { recognition.stop(); } catch (e) { /* ignore */ }
    }
    vozAtiva = false;
    localStorage.removeItem('vozAtiva');
    atualizarIndicador(false);
    console.log('🔇 reconhecimento parado');
}

// Toggle (botão)
function alternarComandosVoz() {
    if (vozAtiva) {
        pararReconhecimento();
    } else {
        iniciarReconhecimento().then(ok => {
            if (!ok) {
                // caso o navegador bloqueie start() por falta de gesto, informe o usuário
                alert('Não foi possível ativar o microfone automaticamente. Clique no botão novamente para permitir o uso do microfone (o navegador pode pedir permissão).');
            }
        });
    }
}

// Indicação visual simples (crie um elemento com id="voz-indicador" no layout ou ajusta conforme precisar)
function atualizarIndicador(ativo) {
    const el = document.getElementById('voz-indicador');
    if (!el) return;
    if (ativo) {
        el.style.display = 'inline-block';
        el.textContent = '🎤 Voz ativa';
        el.classList.add('ativo');
    } else {
        el.style.display = 'none';
        el.classList.remove('ativo');
    }
}

// Ler estado salvo no load e tentar reativar
document.addEventListener('DOMContentLoaded', () => {
    const estavaAtivo = localStorage.getItem('vozAtiva') === 'true';
    // Cria o indicador se quiser (insira no HTML para melhor controle)
    atualizarIndicador(false);

    if (estavaAtivo) {
        // Tentar iniciar — observe: navegadores podem bloquear start() sem gesto.
        // Chamamos, mas se falhar o usuário deve clicar no botão para permitir.
        iniciarReconhecimento().then(ok => {
            if (!ok) {
                console.log('Reconhecimento não pôde ser iniciado automaticamente (possível bloqueio do navegador).');
                // ainda mantemos a preferência no localStorage para tentar reiniciar nas próximas loads,
                // mas pode ser bom avisar o usuário visualmente.
            }
        });
    }
});

// ------ Exemplo de função que processa os comandos ------
function interpretarComando(comando) {
    if (comando.includes('abrir mapa')) window.location.href = '/Home/Maps';
    else if (comando.includes('abrir ocorrência') || comando.includes('abrir ocorrências')) window.location.href = '/Ocorrencias/Index';
    else if (comando.includes('abrir avaliação') || comando.includes('abrir avaliações')) window.location.href = '/Avaliacoes/Index';
    else if (comando.includes('noturno')) document.body.classList.toggle('modo-noturno');
    else if (comando.includes('contraste')) document.body.classList.toggle('alto-contraste');
    else if (comando.includes('aumentar fonte')) alterarFonte(1.1);
    else if (comando.includes('diminuir fonte')) alterarFonte(0.9);
    else if (comando.includes('resetar') || comando.includes('padrão')) resetarAcessibilidade();
    else if (comando.includes('parar leitura')) window.speechSynthesis.cancel();
    else if (comando.includes('abrir home') || comando.includes('abrir home')) window.location.href = '/Home/Index';
    else console.log('Comando não mapeado:', comando);
}
// 🎤 Abrir / Fechar manual de comandos de voz
document.addEventListener("DOMContentLoaded", () => {
    const abrirManual = document.getElementById("abrirManualVoz");
    const fecharManual = document.getElementById("fecharManualVoz");
    const modal = document.getElementById("manualVozModal");

    if (!abrirManual || !fecharManual || !modal) return;

    // 👉 Abrir o modal
    abrirManual.addEventListener("click", () => {
        modal.hidden = false;
        abrirManual.setAttribute("aria-expanded", "true");

        // Animação de entrada
        modal.querySelector(".manual-conteudo").classList.add("mostrar");
    });

    // 👉 Fechar o modal pelo botão interno
    fecharManual.addEventListener("click", fecharModal);

    // 👉 Fechar clicando fora do conteúdo
    modal.addEventListener("click", (e) => {
        if (e.target === modal) fecharModal();
    });

    // 👉 Função para fechar o modal (reutilizável)
    function fecharModal() {
        const conteudo = modal.querySelector(".manual-conteudo");
        conteudo.classList.remove("mostrar");
        setTimeout(() => {
            modal.hidden = true;
            abrirManual.setAttribute("aria-expanded", "false");
        }, 200); // pequeno delay para animação
    }
});

