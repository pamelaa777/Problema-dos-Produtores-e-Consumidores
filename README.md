# Problema-dos-Produtores-e-Consumidores
Implemente um programa que simule o funcionamento de um restaurante. Nesse restaurante existem duas entidades, o Garçom, que cria pedidos, e o Chef, que recebe os pedidos e prepara os pratos.

Existem 3 tipos de pratos
Preto executivo (1 porção de arroz, 1 porção de carne)
Prato italiano (1 porção de macarrão, 1 porção de molho)
Prato especial (1 porção de arroz, 1 porção de carne, 1 porção de molho)
O Chef precisa preparar essas porções antes de montar o prato, mas faz a produção em lotes fixos:
Arroz: 3 porções por preparação
Carne: 2 porções por preparação
Macarrão: 4 porções por preparação
Molho: 2 porções por preparação
Sempre que não houver algum dos componentes do prato prontos, o Chef deve preparar esse componente.

Tempos de Execução:
O garçom deve criar pedidos em tempos aleatórios entre 1 e 10 segundos. O prato também deve ser escolhido aleatoriamente.

O Chef gasta 1 segundo por porção usada na montagem do prato. Caso precise preparar algum componente, ele gasta 2 segundos por componente.

Simulação:
Sempre que coletas um pedido, o garçom deve imprimir na tela em cor azul:
"[Garçom] - Envio de Pedido N: Prato X"

Sempre que o Chef começa e finaliza montagem do prato, deve imprimir na tela em vermelho:
"[Chef] Inicio da Preparação do Pedido N"
"[Chef] Fim da Preparação do Pedido N"

Sempre que o Chef precisar cozinhar algum componente do prato, ele deve imprimir na tela e verde:
"[Chef] Iniciando produção de X"
"[Chef] Finalizou produção de X. Estoque atualizado: Y unidades"

Implemente em C# uma solução com 1 Chef e 1 Garçom, depois faça a implementação com 3 Chefs e 5 Garçons