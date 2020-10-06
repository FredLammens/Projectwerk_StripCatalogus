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
![uml](https://user-images.githubusercontent.com/23512215/95238609-09150600-080a-11eb-8d9f-49b645af5a18.jpg)

#### DomeinOntwerp:    
![DomainLayer_uml](https://user-images.githubusercontent.com/23512215/95238605-087c6f80-080a-11eb-8715-ea81164724e1.jpg)

#### DataLayerOntwerp:    
![DataLayer_uml](https://user-images.githubusercontent.com/23512215/95238610-09150600-080a-11eb-8280-445b9dbc469b.jpg)

#### ViewModelOntwerp :    
![ViewModel_uml](https://user-images.githubusercontent.com/23512215/95238608-09150600-080a-11eb-8594-f74b4cddc11c.jpg)
