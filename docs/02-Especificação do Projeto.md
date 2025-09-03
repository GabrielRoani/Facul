# Especificações do Projeto

<span style="color:red">Pré-requisitos: <a href="1-Documentação de Contexto.md"> Documentação de Contexto</a></span>

## Personas

<img src ="img/1.png">
<img src ="img/2.png">
<img src ="img/3.png">
<img src ="img/4.png">
<img src ="img/5.png">
<img src ="img/6.png">

>
## Histórias de Usuários  

Com base na análise das personas, foram identificadas as seguintes histórias de usuários:  

| EU COMO... `PERSONA`            | QUERO/PRECISO ... `FUNCIONALIDADE`                                    | PARA ... `MOTIVO/VALOR`                                                         |
|---------------------------------|-----------------------------------------------------------------------|---------------------------------------------------------------------------------|
| Ana Paula (Mobilidade reduzida) | Registrar facilmente barreiras de acessibilidade                      | Para que outras pessoas possam evitar dificuldades e o poder público possa agir |
| Ana Paula (Mobilidade reduzida) | Anexar fotos ou vídeos ao registrar uma ocorrência                     | Para dar mais clareza à situação e facilitar a ação do poder público             |
| José Souza (Mobilidade reduzida)| Visualizar locais acessíveis próximos a mim                            | Para planejar meus trajetos com segurança e autonomia                           |
| José Souza (Mobilidade reduzida)| Visualizar rotas acessíveis no mapa em tempo real                      | Para escolher o melhor caminho e evitar barreiras inesperadas                   |
| Larissa Gomes (Cidadã)          | Avaliar locais que frequento                                           | Para contribuir com informações úteis à comunidade                              |
| Larissa Gomes (Cidadã)          | Editar ou excluir avaliações que já fiz                                | Para manter as informações atualizadas e úteis para a comunidade                |
| Marcos Silva (Gestor municipal) | Acessar relatórios consolidados de ocorrências                        | Para priorizar obras e justificar investimentos em acessibilidade               |
| Marcos Silva (Gestor municipal) | Exportar relatórios de acessibilidade em PDF/planilha                  | Para apresentar dados de forma clara em reuniões e planejamentos                |
| Julia Mendes (Mãe de bebê)      | Verificar locais com estrutura adequada para as necessidades do filho | Para planejar deslocamentos com segurança e conforto                            |
| Julia Mendes (Mãe de bebê)      | Visualizar no mapa locais com banheiros adaptados e áreas de apoio familiar | Para planejar deslocamentos de forma mais prática com meu filho             |
| João Batista (Aposentado)       | Visualizar caminhos com menor risco de quedas e maior acessibilidade  | Para me deslocar com segurança e independência                                  |
| João Batista (Aposentado)       | Consultar avaliações de outros usuários sobre acessibilidade de calçadas e praças | Para escolher caminhos mais seguros e adequados à minha mobilidade      |



## Requisitos

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto.

### Requisitos Funcionais

|ID    | Descrição do Requisito                                                                                                                   | Prioridade |
|------|------------------------------------------------------------------------------------------------------------------------------------------|------------|
|RF-001| Permitir ao usuário registrar ocorrências de acessibilidade, com foto, GPS, categoria, severidade (1–5) e descrição.      | ALTA       | 
|RF-002| Disponibilizar mapa com pontos de acessibilidade reportados, permitindo consulta por endereço ou geolocalização.       | ALTA       |
|RF-003| Permitir que usuários avaliem locais com notas (1 a 5 estrelas) e comentários opcionais.                                  | MÉDIA      |
|RF-004| Gerar relatórios consolidados de acessibilidade por região, contendo indicadores e mapa georreferenciado.                 | MÉDIA      |
|RF-005| Aplicar filtros simples no mapa por categoria (ex.: rampas, banheiros, calçadas).                                | BAIXA      |
|RF-006| Gerenciar pesquisas no mapa permitindo ao usuário ver os detalhes da ocorrência: categoria, foto e status.          | ALTA       |
|RF-007| Registrar e  consultar locais que possuam estruturas de apoio voltadas a diferentes grupos de usuários.      | MÉDIA      |
|RF-008| Gerenciar personalização de visualização da interface, ajustando o tamanho da fonte para facilitar a leitura. | ALTA       |
|RF-009| Gerenciar cadastros de conta, login e recperação de senhas.                                                                          | ALTA       |


### Requisitos não Funcionais

| ID     | Descrição                                                                                                                                             | Prioridade |
|--------|-------------------------------------------------------------------------------------------------------------------------------------------------------|------------|
| RNF-01 | A plataforma deve garantir segurança e proteção dos dados dos usuários.                                                                               | ALTA       |
| RNF-02 | A interface deve ser intuitiva e exigir pouco conhecimento tecnológico para realizar o registro.                                                      | ALTA       |
| RNF-03 | O sistema deve seguir as diretrizes WCAG 2.1, oferecendo suporte a leitores de tela, contraste adequado e fontes redimensionáveis.                    | ALTA       |
| RNF-04 | O sistema deve ser responsivo e acessível em dispositivos móveis e desktops.                                                                          | ALTA       |
| RNF-05 | O sistema deve ser compatível com os navegadores mais utilizados (Chrome, Firefox e Edge).                                                            | ALTA       |
| RNF-06 | O código deve ser organizado e documentado para facilitar evolução (ex: categorias adicionais de acessibilidade).                                     | ALTA       |
| RNF-07 | Não devem ser coletados dados pessoais sensíveis, com um aviso na área de fotos anexadas, para que sejam anonimizadas.                                | ALTA       |
| RNF-08 | O sistema deve ser estável e oferecer disponibilidade contínua.                                                                                       | ALTA       |


## Restrições

O projeto está restrito pelos itens apresentados na tabela a seguir.

| ID    | Restrição                                                                                                                                            |
|-------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| RES01 | O aplicativo deve armazenar apenas dados necessários para o funcionamento (evitando coleta excessiva de informações pessoais).                       |
| RES02 | O aplicativo não será totalmente acessível a todos os públicos no lançamento, devido a restrições de tempo e recursos da equipe.                     |
| RES03 | O upload de imagens será limitado em tamanho e quantidade (ex.: até 2 fotos por ocorrência, 5 MB cada).                                              |
| RES04 | O aplicativo não fará reconhecimento facial nas fotos carregadas.                                                                                    |

## Diagrama de Casos de Uso

<img src ="img/diagrama.png">

## Vídeo de apresentação do projeto

https://github.com/user-attachments/assets/2d79c916-ea7e-46d6-b010-6162e34d6315
