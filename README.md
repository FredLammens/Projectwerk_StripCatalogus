# Projectwerk_StripCatalogus

## Rolverdeling:
**Organisatie:** Verbraeken Niels  
**Testing:** Djabrailova Khava  
**Architectuur:** Lammens Frederic  
**Business** Frans Aaron

## Technisch:
* 1 solution met verschillende projecten
* Server: SQL-Server
* ADO.NET
* Github
* WPF
* Config in file
* Unit Tests
* Voldoende Documentatie

## Deel 1: StripCatalogus

| Strip               | Vb.                               |
| ------------------- |:---------------------------------:|
| Titel               | Asterix en de Gothen              |
| Reeks + nummer      | Asterix, Nr: 6                    |
| Auteurs             | Gosciny René & Uderzo Albert      |
| Uitgeverij          | Dargaud                           |

### User requirements:
1) Strips toevoegen aan catalogus
    * Hulp bij invoegen (autocomplete)
    * GEEN DUBBELS (Strips, auteurs, uitgeverij)
    * Aanpassingen strips op een gebruikvriendelijke manier
    * Aanpassingen strips => moeten eenvoudig op te zoeken zijn
2) Catalogus importeren/exporteren (JSON bestand)  

### Ontwerp:    
![ontwerp](https://user-images.githubusercontent.com/23512215/95356236-bd746200-08c6-11eb-98ca-1680e7de5305.jpg)

#### DomeinOntwerp:    
![DomeinLayer](https://user-images.githubusercontent.com/23512215/95356234-bcdbcb80-08c6-11eb-91d6-2139d3b61aab.jpg)

#### DataLayerOntwerp:    
![DataLayere](https://user-images.githubusercontent.com/23512215/95356227-bc433500-08c6-11eb-9af9-f63ffdead37d.jpg)

#### ViewModelOntwerp :    
![ViewModel](https://user-images.githubusercontent.com/23512215/95356235-bd746200-08c6-11eb-8500-a116c7f2dc6a.jpg)
