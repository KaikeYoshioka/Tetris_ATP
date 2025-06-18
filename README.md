# Tetris_ATP

1 Projeto de Desenvolvimento do Jogo Tetris
1.1 Objetivo
Desenvolver uma versão simplificada do clássico jogo Tetris, visando aplicar conceitos fundamentais de pro-
gramação, como matrizes, manipulação de arquivos, controle de fluxo, modularização e interação com usuá-
rios.
1.2 Descrição Geral
O jogo deve conter blocos formados por combinações geométricas (tetrominós) que caem de cima para baixo
em um tabuleiro representado por uma matriz. O jogador deve controlar esses blocos, movimentando-
os horizontalmente, rotacionando-os em sentido horário e anti-horário. A descida de cada bloco deve ser
executada pelo usuário
1.3 Funcionalidades
1. Tabuleiro
• Utilizar uma matriz bidimensional como base do jogo (recomendação inicial: matriz 20 × 10).
2. Tetrominós
• Implementar pelo menos três formatos clássicos (I, L, T).
• Cada tetrominó deve ter duas funcionalidades principais de rotação: sentido horário e anti-horário.
3. Controles
• Movimentação lateral para esquerda e direita.
• Rotação no sentido horário e anti-horário.
• Descida controlada pelo usuário.
• Opcional 1: descida controlada de forma automática.
• Opcional 2: aumento da aceleração da descida conforme a pontuação aumenta.
4. Pontuação
• Cada linha completa removida concede pontos ao jogador (ex.: 100 pontos por linha).
• Pontuação adicional por remover múltiplas linhas simultaneamente.
5. Fim de Jogo
• O jogo termina quando um novo tetrominó não puder mais entrar completamente no tabuleiro.
6. Registro de Pontuação
• Gravar o nome do jogador e sua respectiva pontuação final em um arquivo texto (scores.txt).
• Exemplo de formato no arquivo: NomeJogador;Score
1
1.4 Requisitos Técnicos
• Utilizar linguagens de programação C#.
• Entrada de dados do usuário (nome e comandos de controle).
• Utilização obrigatória de matrizes para representar o tabuleiro e os tetrominós.
• Código deve ser escrito de forma clara, não deve ser código espaguete.
• Manipulação de arquivos em formato texto para armazenamento das pontuações.
• Uso de modularização.
• A classe Tetrominos deve representar ao tetrominós com seus métodos e atributos.
1.5 Sugestões de Implementação
• Implementar uma interface simples em modo texto (console).
• Estruturar o código usando funções ou métodos específicos para cada funcionalidade:
– Ex.: funções para desenhar o tabuleiro, mover tetrominós, verificar colisões, rotacionar peças,
registrar pontuações, etc.
A seguir é apresentada uma possível interface para o problema:

                    |                   |
                    |                   |
                    |                   |
                    |     *             |
                    |     *             |
                    |     *             |
                    |                   |
                    |       #           |
                    |  @    #           |
                    | @@@  ###          |
                    |___________________|
   
1.6 Critérios de Avaliação
• Funcionamento correto da movimentação e rotação dos tetrominós.
• Precisão na detecção de linhas completas e contabilização dos pontos.
• Qualidade e clareza do código-fonte.
• Cumprimento da gravação e leitura do arquivo de pontuações.
• Usabilidade e facilidade de controle pelos usuários.
1.7 Entrega
• Código-fonte comentado.
• Arquivo texto (scores.txt) com registros dos testes realizados.
