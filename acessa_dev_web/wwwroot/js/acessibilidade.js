// ======== MENU DE ACESSIBILIDADE ========
document.addEventListener("DOMContentLoaded", () => {
    const toggleBtn = document.getElementById("toggleAcessibilidade");
    const opcoes = document.getElementById("acessibilidade-opcoes");

    if (toggleBtn && opcoes) {
        toggleBtn.addEventListener("click", () => {
            const expanded = toggleBtn.getAttribute("aria-expanded") === "true";
            toggleBtn.setAttribute("aria-expanded", !expanded);
            opcoes.hidden = expanded;
        });
    }

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
// 🎙️ Controle por Voz com Web Speech API
let reconhecimentoVoz = null;
let vozAtiva = false;

function alternarComandosVoz() {
    if (!('webkitSpeechRecognition' in window)) {
        alert("❌ O seu navegador não suporta reconhecimento de voz.");
        return;
    }

    if (!reconhecimentoVoz) {
        reconhecimentoVoz = new webkitSpeechRecognition();
        reconhecimentoVoz.lang = "pt-BR";
        reconhecimentoVoz.continuous = true;
        reconhecimentoVoz.interimResults = false;

        reconhecimentoVoz.onresult = function (event) {
            const ultima = event.results[event.results.length - 1];
            const comando = ultima[0].transcript.toLowerCase().trim();
            console.log("🎧 Comando detectado:", comando);

            // 🔊 Mapeamento dos comandos de voz
            if (comando.includes("noturno")) modoNoturno();
            else if (comando.includes("contraste")) alternarContraste();
            else if (comando.includes("aumentar fonte")) alterarFonte(1.1);
            else if (comando.includes("diminuir fonte")) alterarFonte(0.9);
            else if (comando.includes("leitura")) ativarLeituraPorHover();
            else if (comando.includes("resetar") || comando.includes("padrão")) resetarAcessibilidade();
            else if (comando.includes("parar leitura")) window.speechSynthesis.cancel();
            else if (comando.includes("daltonismo")) modoDaltonismo();
            else if (comando.includes("abrir mapa")) {
                window.location.href = "/Home/Maps";
            }
            else if (comando.includes("abrir ocorrência") || comando.includes("abrir ocorrências")) {
                window.location.href = "/Ocorrencias/Index";
            }
            else if (comando.includes("abrir avaliação") || comando.includes("abrir avaliações")) {
                window.location.href = "/Avaliacoes/Index";
            }

            else {
                console.log("🗣️ Comando não reconhecido:", comando);
            }
        };

        reconhecimentoVoz.onerror = function (event) {
            console.warn("Erro de voz:", event.error);
        };

        reconhecimentoVoz.onend = function () {
            if (vozAtiva) reconhecimentoVoz.start(); // reinicia automaticamente
        };
    }

    // Ativa/desativa
    vozAtiva = !vozAtiva;

    if (vozAtiva) {
        reconhecimentoVoz.start();
        alert("🎙️ Controle por voz ativado. Fale comandos como 'modo noturno', 'contraste', 'aumentar fonte'...");
    } else {
        reconhecimentoVoz.stop();
        alert("🛑 Controle por voz desativado.");
    }
}
// 🎤 Abrir / Fechar manual de comandos de voz
document.addEventListener("DOMContentLoaded", () => {
    const abrirManual = document.getElementById("abrirManualVoz");
    const fecharManual = document.getElementById("fecharManualVoz");
    const modal = document.getElementById("manualVozModal");

    if (abrirManual && modal && fecharManual) {
        abrirManual.addEventListener("click", () => {
            modal.hidden = false;
            abrirManual.setAttribute("aria-expanded", "true");
        });

        fecharManual.addEventListener("click", () => {
            modal.hidden = true;
            abrirManual.setAttribute("aria-expanded", "false");
        });

        // Fechar clicando fora do modal
        modal.addEventListener("click", (e) => {
            if (e.target === modal) {
                modal.hidden = true;
                abrirManual.setAttribute("aria-expanded", "false");
            }
        });
    }
});
// ----------------------
// 🎙️ Reconhecimento de voz
// ----------------------
let reconhecimentoAtivo = false;
let recognition;

function iniciarReconhecimento() {
    if (!('webkitSpeechRecognition' in window)) {
        alert("Seu navegador não suporta reconhecimento de voz.");
        return;
    }

    recognition = new webkitSpeechRecognition();
    recognition.lang = 'pt-BR';
    recognition.continuous = true;
    recognition.interimResults = false;

    recognition.onresult = function (event) {
        const comando = event.results[event.results.length - 1][0].transcript.toLowerCase().trim();
        console.log("🗣️ Comando:", comando);
        interpretarComando(comando);
    };

    recognition.onend = function () {
        if (reconhecimentoAtivo) {
            recognition.start(); // reinicia automaticamente se ainda estiver ativo
        }
    };

    recognition.start();
    reconhecimentoAtivo = true;
    localStorage.setItem('vozAtiva', 'true');
    console.log("🎤 Voz ativada.");
}

function pararReconhecimento() {
    if (recognition) recognition.stop();
    reconhecimentoAtivo = false;
    localStorage.removeItem('vozAtiva');
    console.log("🔇 Voz desativada.");
}

// ----------------------
// 🧠 Reativar após reload
// ----------------------
document.addEventListener('DOMContentLoaded', () => {
    const estavaAtivo = localStorage.getItem('vozAtiva') === 'true';
    if (estavaAtivo) {
        iniciarReconhecimento();
    }
});
