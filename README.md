# Maze resolving by IA (Genetic Algorithm)
Create a maze in unity that resolve itself with IA (machine learning)

## Getting Started
The project has been created with [Visual Studio 2022](https://visualstudio.microsoft.com/fr/vs/) as a Text Editor and [Unity](https://unity.com/fr) for all the rest of the program.

### Before downloading

- The project was created with version `2021.3.3f1` of unity, make sure you use this version.

### Installing 

- `$ git clone https://github.com/po79tvx/Projet_Cs_Unity_Maze.git`
- Or download directly the release

## Various classes

| Classe | Description |
| --- | --- |
| [`GeneratePop`](#Population) | Main classe where everything is instantiate |
| [`Algorithm`](#Algorithm) | The genetic algorithm, who create the population |
| [`DOT`](#DOT) | A member of the population |
| [`DNA`](#DNA) | The brain of the dot |

## Functions

<a name="Population"></a>
### Generate Pop
| Name | Description |
| --- | --- |
| CalculatePopulationFitness() | Calculate the fitness of the population |
| NewGeneration(int, bool) | Create a new generation of dot |
| DNA Crossover(DNA, DNA) | Make a crossover between two parents |
| DOT ChooseParent() | The brain of the dot |

<a name="Algorithm"></a>
### Algorithm 

<!-- 
Functions from Algorithm classe
-->

<a name="DOT"></a>
### DOT 

<!-- 
Functions from DOT classe
-->

<a name="DNA"></a>
### DOT 

<!-- 
Functions from DNA classe
-->


## Annexes

- Requirements specification (Made by me)[^1]
- Logbook[^2]


[^1]: Soon
[^2]: Soon

