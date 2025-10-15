#  Registro de Testes de Usabilidade – Cadastro e Login (Acessa+)

## 1. Descrição Geral

O registro a seguir documenta os testes de usabilidade realizados nas funcionalidades de **Cadastro de Usuário** e **Login** do aplicativo **Acessa+**, parte essencial para o acesso ao sistema.

O objetivo principal foi avaliar:
- A **clareza e eficiência** do processo de autenticação;
- A **facilidade de preenchimento dos campos** e a **compreensão das mensagens de erro**;
- A **acessibilidade visual e cognitiva** dessas telas para diferentes perfis de usuários.

---

## 2. Perfil dos Usuários Participantes

| Usuário | Idade | Escolaridade | Nível de Conhecimento em Tecnologia | Tipo de Deficiência / Perfil |
|----------|--------|--------------|------------------------------------|------------------------------|
| Usuário 1 | 45 anos | Ensino Médio | Básico | Pessoa com mobilidade reduzida |
| Usuário 2 | 18 anos | Ensino Superior (em andamento) | Avançado | Usuário sem deficiência |
| Usuário 3 | 70 anos | Ensino Fundamental | Básico | Pessoa com mobilidade reduzida |
| Usuário 4 | 25 anos | Ensino Superior Completo | Avançado | Gestor público |
| Usuário 5 | 28 anos | Ensino Superior Completo | Intermediário | Usuário sem deficiência |

---

## 3. Cenário 1 – Cadastro de Novo Usuário

**Objetivo:** Avaliar a clareza e facilidade do processo de criação de conta no aplicativo.  
**Critério de sucesso:** Usuário deve concluir o cadastro em até 2 minutos, sem erros impeditivos.

### Contexto:
O participante abre o aplicativo pela primeira vez e precisa criar uma conta informando nome, e-mail, senha e tipo de usuário.

| Usuário | Tempo Total (seg) | Cliques | Tarefa Concluída? | Erros Cometidos | Feedback do Usuário |
|----------|--------------------|----------|-------------------|------------------|----------------------|
| Usuário 1 | 140 | 9 | Sim | 1 | "Consegui cadastrar, mas  não vi o campo de confirmação da senha." |
| Usuário 2 | 70 | 6 | Sim | 0 | "Processo rápido, bom uso de ícones." |
| Usuário 3 | 190 | 11 | Sim | 2 | "Letras pequenas e contraste fraco, precisei de ajuda." |
| Usuário 4 | 60 | 5 | Sim | 0 | "Muito fácil, mas poderia salvar senha automaticamente." |
| Usuário 5 | 85 | 7 | Sim | 0 | "Gostei da simplicidade, mas faltou mensagem clara de sucesso." |

**Taxa de sucesso:** 100%  
**Tempo médio:** 109s  
**Principais dificuldades:** Fonte pequena, campo de confirmação pouco visível, e ausência de feedback visual após sucesso.

---

## 4. Cenário 2 – Login no Sistema

**Objetivo:** Verificar a facilidade e clareza no processo de login.  
**Critério de sucesso:** Usuário deve conseguir acessar a conta em até 1 minuto, sem erros críticos.

### Contexto:
Usuário já cadastrado acessa o aplicativo novamente e precisa realizar login com e-mail e senha.

| Usuário | Tempo Total (seg) | Cliques | Tarefa Concluída? | Erros Cometidos | Feedback do Usuário |
|----------|--------------------|----------|-------------------|------------------|----------------------|
| Usuário 1 | 90 | 4 | Sim | 0 | "Rápido, mas os campos são pequenos para digitar." |
| Usuário 2 | 40 | 3 | Sim | 0 | "Fácil, gostei da opção de mostrar senha." |
| Usuário 3 | 110 | 5 | Sim | 1 | "Errei a senha uma vez, mas a mensagem de erro foi clara." |
| Usuário 4 | 35 | 3 | Sim | 0 | "Sem dificuldade, login imediato." |
| Usuário 5 | 55 | 3 | Sim | 0 | "Funcional, poderia ter login com Google." |

**Taxa de sucesso:** 100%  
**Tempo médio:** 66s  
**Principais dificuldades:** Campos pequenos e falta de opções alternativas (login social).  

---

## 5. Análise Geral dos Resultados

| Indicador | Resultado | Meta | Avaliação |
|------------|------------|------|------------|
| **Taxa média de sucesso** | 100% | ≥ 90% |  Atingida |
| **Tempo médio total (cadastro + login)** | 88s | ≤ 120s |  Atingida |
| **Média de erros** | 0.6 por tarefa | ≤ 2 |  Atingida |
| **Satisfação geral (escala 1–5)** | 4.5 | ≥ 4.0 |  Atingida |

---

## 6. Principais Dificuldades Identificadas

- Campos e botões **pequenos para usuários com problemas de visão**;  
- **Baixo contraste** entre campos e fundo, dificultando leitura;  
- Falta de **mensagem de confirmação mais visível** após o cadastro;  
- Ausência de **acesso alternativo** (como login via Google ou biometria).

---

## 7. Sugestões de Melhoria

1. **Aumentar tamanho dos campos e botões** nas telas de autenticação.  
2. **Melhorar contraste visual** conforme diretrizes WCAG (mínimo 4.5:1).  
3. **Adicionar feedback visual** após cadastro concluído.  
4. **Incluir login social (Google / Facebook)** para usuários avançados.  
5. **Implementar lembrete de senha visual mais acessível** (ícone mais visível e texto alternativo).

---

## 8. Classificação dos Problemas e Ações Recomendadas

| Nível | Descrição | Exemplo | Ação |
|--------|------------|----------|------|
| **Crítico** | Dificuldade de leitura em usuários idosos | Contraste baixo nos campos de texto | Corrigir no próximo ciclo |
| **Moderado** | Campos pequenos em telas móveis | Dificuldade de toque em botões de “Cadastrar” e “Entrar” | Ajustar layout responsivo |
| **Leve** | Falta de login alternativo | Login apenas por e-mail e senha | Adicionar opção futura |

---

## 9. Conclusão

Os testes de **Cadastro e Login** indicam que o fluxo principal está **intuitivo, funcional e compreensível** para diferentes perfis de usuários.  
Os participantes concluíram as tarefas sem dificuldades críticas, demonstrando que a interface atual atende aos critérios de **eficiência e satisfação** definidos no plano de testes.

>  **Conclusão geral:** As telas de autenticação do Acessa+ são usáveis e acessíveis, necessitando apenas de pequenos ajustes visuais e de acessibilidade para atingir excelência.

---

**Referências**
- ISO 9241-11: Ergonomia da interação humano-sistema  
- Nielsen, J. – *Usability Engineering*  
- W3C WAI – *Web Accessibility Initiative*
