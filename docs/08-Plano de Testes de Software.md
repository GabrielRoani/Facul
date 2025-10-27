# Plano de Testes de Software

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="4-Projeto de Interface.md"> Projeto de Interface</a>

A seguir são apresentados os cenários de testes funcionais da aplicação, elaborados com base nos requisitos definidos no projeto.

#ETAPA 03

| **Caso de Teste** | **CT-01 – Cadastro de novo usuário** |
| :---:              | :---:                                                                                               |
| **Requisito Associado** | RF-009 - A aplicação deverá ter um sistema de cadastro de conta e login.                         |
| **Objetivo do Teste** | Verificar se um novo usuário consegue se cadastrar com sucesso no sistema.                        |
| **Passos** | 1. Acessar a página inicial.<br>2. Clicar na opção "Cadastrar novo usuário".<br>3. Preencher os campos do formulário (Nome, Senha).<br>4. Clicar no botão "Criar Conta". |
| **Critério de Êxito** | O sistema deve validar os dados, criar a conta e exibir uma mensagem de "Cadastro realizado com sucesso", redirecionando para a tela de login. |
|                    |                                                                                                     |
| **Caso de Teste** | **CT-02 – Efetuar login de usuário** |
| **Requisito Associado** | RF-009 - A aplicação deverá ter uma tela para logar e autenticar o usuário.                          |
| **Objetivo do Teste** | Verificar se um usuário já cadastrado consegue se autenticar com sucesso.                         |
| **Passos** | 1. Acessar a tela de login.<br>2. Informar um usuário cadastrado.<br>3. Informar a senha correspondente.<br>4. Clicar no botão "Entrar". |
| **Critério de Êxito** | O login deve ser realizado com sucesso, e o usuário deve ser redirecionado para a tela principal (mapa). |
|#FIM ETAPA 03
| **Caso de Teste** | **CT-03 – Registrar ocorrência de acessibilidade** |
| **Requisito Associado** | RF-001 - Permitir ao usuário registrar ocorrências de acessibilidade, com foto, GPS, categoria, severidade (1–5) e descrição. |
| **Objetivo do Teste** | Verificar se um usuário logado consegue registrar uma nova ocorrência de acessibilidade.           |
| **Passos** | 1. Efetuar login no sistema.<br>2. Navegar até a função "Registrar Nova Ocorrência".<br>3. Preencher os campos obrigatórios (categoria, severidade, descrição).<br>4. Anexar uma foto.<br>5. Permitir o uso do GPS para capturar a localização.<br>6. Clicar em "Salvar". |
| **Critério de Êxito** | A ocorrência deve ser salva com sucesso e exibida como um novo ponto no mapa, conforme o critério de verificação. |
|                    |                                                                                                     |
| **Caso de Teste** | **CT-04 – Consultar detalhes de uma ocorrência no mapa** |
| **Requisito Associado** | RF-006 - Permitir que, ao clicar em um ponto do mapa, o usuário veja os detalhes da ocorrência: categoria, foto e status. |
| **Objetivo do Teste** | Verificar se os detalhes de uma ocorrência são exibidos corretamente ao usuário.                   |
| **Passos** | 1. Acessar o mapa da aplicação.<br>2. Clicar em um dos pontos de ocorrência existentes.<br>3. Observar a janela ou pop-up que é exibida. |
| **Critério de Êxito** | A janela deve exibir corretamente todos os dados cadastrados da ocorrência, como categoria, foto e status. |
|                    |                                                                                                     |
| **Caso de Teste** | **CT-05 – Alterar opções de visualização (Acessibilidade)** |
| **Requisito Associado** | RF-008 - O sistema deve permitir que os usuários personalizem a visualização da interface, ajustando o tamanho da fonte e o contraste das cores para facilitar a leitura. |
| **Objetivo do Teste** | Verificar se o usuário consegue alterar o tamanho da fonte e o tema de cores.                        |
| **Passos** | 1. Acessar o menu de "Configurações" ou "Perfil".<br>2. Localizar as opções de "Acessibilidade" ou "Visualização".<br>3. Selecionar um tamanho de fonte maior.<br>4. Ativar o modo de alto contraste.<br>5. Navegar para outra página e verificar a aplicação das mudanças. |
| **Critério de Êxito** | A interface da aplicação deve refletir imediatamente as alterações de fonte e contraste selecionadas pelo usuário. |
