# Simulação de Fluxo: Terra vs Titã (OpenFOAM + Unity)

Projeto desenvolvido para a disciplina de Computação Científica 2. O objetivo foi criar uma visualização interativa que compara como um foguete se comporta aerodinamicamente na Terra e em Titã (lua de Saturno).

A ideia principal é entender como as ondas de choque geradas na geometria de um foguete se comportam com parâmetros que simulam a terra e Titã  voando na mesma velocidade.

## Como Ver a Animação sem Precisar de Nada (Download)
Eu gerei um executável (Linux) para quem quiser ver a animação sem precisar instalar o Unity. Basta extrair o zip e rodar o executável
* **[Baixar a Versão Linux aqui](https://github.com/leonardod16p/openfoam-unity-rocket-flow/releases/tag/v1.0)**
  * Sistema: Arch Linux
  * Kernel: Linux 6.17.9-arch1-1
  * Arquitetura: x86_64

## Preview

![Preview](demoAnimacao.gif)



## Sobre o Projeto
O foguete voa a 1000 m/s nos dois casos (na verdade, o fluido escoa na direção do foguete nessa velocidade). O recorte da onda de choque na Terra foi feito com o filtro Contour no ParaView a aproximadamente 580.000 Pa, e em Titã, a cerca de 2.300.000 Pa.
* Terra: O som viaja a ~343 m/s. O foguete está a Mach 2.9.
* Titã: É muito frio (94K) e denso, então o som viaja mais devagar (~194 m/s). O mesmo foguete atinge Mach 5.15 (Hipersônico).

**Resultado Visual:**
No OpenFOAM, isso muda o ângulo da onda de choque. Em Titã, o cone de choque fica bem mais fechado ("colado" no foguete) por causa do Mach elevado. Na terra, o cone se estabiliza com um formato de prato.



## Ferramentas Usadas

1.  **OpenFOAM (rhoCentralFoam/snappyHexMesh):**
    * Usei para discretizar a geometria do foguete, calcular o escoamento compressível e gerar os dados físicos.
2.  **ParaView:**
    * Usei para visualizar os resultados e exportar a malha da onda de choque.
3.  **Unity 3D:**
    * De forma geral, usei para criar uma animação utilizando o que eu havia gerado de dados físicos e o que o ambiente Unity me dava.


## Estrutura das Pastas
`/OpenFOAM`: Contém os arquivos de configuração da simulação (0, constant, system).

`/OpenFOAM/*/constant/polyMesh/`: Contém os arquivos de malha gerados pelo openFOAM.

`/modelo3dFoguete`: Modelo 3d do foguete com o link de origem.

`/Unity`: Projeto completo com os scripts e cenas.

`/Unity/Assets/Meshes_Terra/`: Aqui estão as ondas de choque (.obj) geradas com os parâmetros da Terra importadas do ParaView.

`/Unity/Assets/Meshes_Titan/`: Aqui estão as ondas de choque (.obj) geradas com os parâmetros de Titã importadas do ParaView.
    
`/Unity/Assets/Scenes/SampleScene.unity`: Arquivo da cena principal do projeto.



## Comandos de Execução

Sequência de comandos utilizada no terminal para gerar a malha e rodar a simulação CFD:

```bash
blockMesh
surfaceFeatureExtract
snappyHexMesh -overwrite
checkMesh
decomposePar
mpirun -np 6 rhoCentralFoam -parallel
reconstructPar
```

## Dados Brutos da Simulação
O repositório contém apenas os arquivos dos parâmetros da simulação.
Se você quiser baixar os dados gerados pelo comando rhoCentralFoam que usei para extrair a onda de choque clique no link abaixo:
* **[Baixar Dados no Google Drive](https://drive.google.com/drive/u/1/folders/1s107-5a55q_kGopmb5tvnwGtqzIK0sPB)** (~) (Estou upando ainda)


---
*Projeto criado por Leonardo.*
