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
![ontwerp](https://user-images.githubusercontent.com/23512215/98315262-217a6b00-1fd8-11eb-8b23-df4db4980d08.jpg)

#### DomeinOntwerp:    
![DomainLayer](https://user-images.githubusercontent.com/23512215/98315258-1f181100-1fd8-11eb-951b-c39e65a0673b.jpg)

#### DataLayerOntwerp:    
![DataLayer](https://user-images.githubusercontent.com/23512215/98315235-11628b80-1fd8-11eb-9a78-047da42e7420.jpg)

#### ViewModelOntwerp :    
![ViewModel](https://user-images.githubusercontent.com/23512215/98315265-22ab9800-1fd8-11eb-92c3-a96681bfc68d.jpg)

## Deel 2: Bundels toevoegen

### Schatting refactoring:

- Analyse : 5u
  - analyse van nieuwe opdracht + eventuele vragen: 1,30u
  - use cases: 2,30u
    - strip corrigeren
    - strip toevoegen 
    - strip verwijderen
  - domainregels: 1u
- Ontwerp : 3u
  - uml's aanpassen.
- Datank: 30u
  - model aanpassen: 2u
  - db model updaten: 5u
  - database queries schrijven en implementeren: 13u
  - tijd voor testen : 10u
  - tijd voor eventuele probelemen die opdruiken 6u
- DomeinLaag: 14u
  - Bundel class toevoegen : 4u
  - Catalogue aanpassen: 10u
    - AddBundel: checkt of comics in lijst van comics staan
    - removeBundel moet toegevoegd worden.
    - remove comic moet aangepast worden : als in bundel enkel uit lijst van comics halen.
- UI: 16u
  - Datagrid aanpassen: 5u
  - Pagina info strips aanpassen: 8u
  - Toevoegen catalogus aanpassen: 3u
- Tests: 10u
  - test AddBundel: 2u
  - test RemoveBundel: 2u
  - test Exceptions: 3u
  - aanpassing andere testen: 2u
  - documentatie: 1u
- Powerpoint: 5u
  - veranderingen op bijna elke pagina van PP (in totaal 63 p.)
    => Analyse, Ontwerp, Domainlayer, Datalayer,ViewModelayer, Testlayer
- Andere: 2u
  - Eventuele extra githubProblemen + 2u

Totaal : 85u
### Deel 3: Inventaris toevoegen 
### Ontwerp:    
![ontwerp](https://user-images.githubusercontent.com/23512215/101981246-b887b200-3c6b-11eb-9fdf-369a9d5d0998.jpg)

#### DomeinOntwerp:    
![Domain](https://user-images.githubusercontent.com/23512215/101981243-b6bdee80-3c6b-11eb-969c-b9574bdf69e6.jpg)

#### DataLayerOntwerp:    
![DataLayer](https://user-images.githubusercontent.com/23512215/101981245-b7ef1b80-3c6b-11eb-94e4-579b455451bd.jpg)

#### ViewModelOntwerp :    
![UI](https://user-images.githubusercontent.com/23512215/101981244-b7ef1b80-3c6b-11eb-8fd3-9e893f2844f9.jpg)
  
