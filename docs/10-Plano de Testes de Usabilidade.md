# Plano de Testes de Usabilidade

Os testes de usabilidade permitem avaliar a qualidade da interface com o usuário da aplicação interativa.

## Definição dos Objetivos

Os objetivos deste plano de testes são:
- Avaliar a facilidade e a eficiência do processo de registro de uma nova ocorrência de acessibilidade.
- Verificar se os usuários conseguem consultar as informações no mapa de forma clara e intuitiva.
- Identificar possíveis barreiras de usabilidade para usuários com diferentes perfis e necessidades.
- Medir o nível de satisfação do usuário ao interagir com as funcionalidades principais da plataforma.

## Seleção dos Participantes

Para os testes, serão selecionados 5 participantes que representem o público-alvo do projeto, incluindo:
- Pessoas com algum tipo de mobilidade reduzida (usuários de cadeira de rodas, muletas, etc.).
- Cidadãos engajados em pautas sociais e que não possuem deficiência.
- Pessoas com diferentes níveis de afinidade com tecnologia (básico, intermediário e avançado).
- Participantes de diferentes faixas etárias.

## Definição dos Cenários de Teste

Foram definidos cinco cenários para representar tarefas reais que os usuários executarão no sistema.

---

### **Cenário 1: Registrar uma barreira urbana**

**Objetivo:** Avaliar a eficiência e clareza do processo de registrar uma barreira urbana, do início ao fim.
**Contexto:** Você é uma pessoa que utiliza cadeira de rodas e se deparou com uma rampa de acesso bloqueada por um veículo. Você decide usar o aplicativo para registrar essa barreira e alertar outras pessoas.
**Tarefa(s):**
1.  Abra o aplicativo e faça login na sua conta.
2.  Inicie o processo para registrar uma nova ocorrência.
3.  Preencha os detalhes: classifique a ocorrência, descreva o problema como "rampa obstruída" e defina a severidade.
4.  Anexe uma foto que você tirou da situação.
5.  Confirme e salve o registro.
**Critério de Sucesso:** O usuário consegue registrar a ocorrência em menos de 2 minutos, sem expressar frustração ou cometer erros que o impeçam de concluir a tarefa.

---

### **Cenário 2: Planejar um trajeto seguro**

**Objetivo:** Testar a eficácia da consulta de locais e ocorrências no mapa para o planejamento de rotas.
**Contexto:** Você precisa ir a um local que não conhece bem. Antes de sair de casa, você quer usar o aplicativo para verificar se o trajeto e o destino são acessíveis.
**Tarefa(s):**
1.  Abra o aplicativo e acesse o mapa.
2.  Procure pelo endereço de destino.
3.  Verifique os pontos de acessibilidade e as barreiras reportadas na área.
4.  Clique em um ponto de ocorrência para ver os detalhes, como a foto e a descrição.
**Critério de Sucesso:** O usuário localiza as informações de acessibilidade da região e consegue tomar uma decisão informada sobre sua rota. As informações apresentadas no mapa e nos detalhes da ocorrência são claras e úteis.

---

### **Cenário 3: Avaliar um estabelecimento comercial**

**Objetivo:** Avaliar a usabilidade do sistema de avaliação de locais.
**Contexto:** Você está em um restaurante que notou ter um excelente banheiro adaptado e bom espaço para circulação. Você quer avaliar o local positivamente para que a comunidade saiba que é um lugar acessível.
**Tarefa(s):**
1.  Encontre o restaurante no mapa do aplicativo.
2.  Inicie o processo de avaliação do local.
3.  Dê uma nota de 5 estrelas.
4.  Adicione um comentário elogiando o banheiro adaptado.
5.  Envie sua avaliação.
**Critério de Sucesso:** O usuário consegue completar a avaliação de forma rápida e intuitiva, sentindo que sua contribuição foi registrada com sucesso.

---

### **Cenário 4: Gerar um relatório de ocorrências (Perfil Gestor)**

**Objetivo:** Verificar a funcionalidade de geração de relatórios para tomada de decisão.
**Contexto:** Você é um gestor público e precisa de dados para justificar um investimento em obras de acessibilidade em um bairro específico. Você usará o sistema para gerar um relatório das ocorrências na região.
**Tarefa(s):**
1.  Acesse a área de relatórios do sistema.
2.  Aplique um filtro para visualizar apenas as ocorrências de um bairro específico.
3.  Gere o relatório em formato PDF/Excel.
4.  Verifique se o relatório contém os dados esperados (mapa, indicadores, lista de problemas).
**Critério de Sucesso:** O gestor consegue gerar um relatório útil e compreensível, que pode ser utilizado para embasar decisões, sem necessidade de conhecimento técnico avançado.

---

### **Cenário 5: Personalizar a interface para melhor leitura**

**Objetivo:** Testar a funcionalidade de personalização da interface, conforme o requisito RF-008.
**Contexto:** Você tem alguma dificuldade de leitura e deseja aumentar o tamanho das letras e o contraste das cores do aplicativo para usá-lo de forma mais confortável.
**Tarefa(s):**
1.  Acesse o menu de "Configurações" ou "Perfil".
2.  Encontre as opções de "Acessibilidade" ou "Visualização".
3.  Aumente o tamanho da fonte para a opção maior.
4.  Altere o tema para o modo de "Alto Contraste".
5.  Volte para a tela do mapa e verifique se as alterações foram aplicadas em todo o aplicativo.
**Critério de Sucesso:** O usuário consegue encontrar e aplicar as configurações de visualização facilmente, e as alterações melhoram sua experiência de leitura na plataforma.