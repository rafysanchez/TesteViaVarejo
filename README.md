# TesteViaVarejo
TESTE TÉCNICO 


Como um programador muito popular, você conhece muitas pessoas em seu país. Como você viaja muito, você decidiu que seria muito útil ter um programa que te dissesse quais de seus amigos estão mais próximos baseado em qual amigo você está atualmente visitando. 

Cada um de seus amigos vive em uma posição específica (latitude e longitude) - para os propósitos deste problema o mundo é plano e a latitude e a longitude são coordenadas cartesianas em um plano - e você consegue identificá-los de alguma maneira. Também cada amigo mora em uma posição diferente (dois amigos nunca estão na mesma latitude e longitude). 


Escreva um programa que receba a localização de cada um dos seus amigos e, para cada um deles, você indique quais são os outros três amigos mais próximos a ele. 


- Criar uma API com todas as regras de cálculo (Usar DDD será um diferencial) 

- Criar uma aplicação que consuma a API 

- A aplicação precisará passar TOKEN para poder acessar os métodos da API. Caso o TOKEN esteja inválido, retornar HTTP_CODE = 401 

- Todos os cálculos realizados (tabela/collection CalculoHistoricoLog) precisam ser armazenados em algum repositório (Sql Server ou mongo) 

- Disponibilizar fontes no GitHub 

- Criar testes unitários dos métodos da api (diferencial) 


Observações: 

- Criar a API em .Net (preferencialmente com Asp Core) 

- A aplicação que consumirá a API poderá ser do tipo console, Windows form ou WebApp ( Angular 5 será um grande diferencial)  

- Disponibilizar o script DDL da tabela de CalculoHistoricoLog  

- As configurações do banco precisam estar no web.config/appsettings.json 

