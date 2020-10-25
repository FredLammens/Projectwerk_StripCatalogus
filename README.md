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
![ontwerp](https://user-images.githubusercontent.com/23512215/95454585-f404b800-096c-11eb-878c-261c50368eac.jpg)

#### DomeinOntwerp:    
![DomainLayer_uml](https://user-images.githubusercontent.com/23512215/95454581-f36c2180-096c-11eb-822e-7451a35e18fd.jpg)

#### DataLayerOntwerp:    
![DataLayer_uml](https://user-images.githubusercontent.com/23512215/95454579-f2d38b00-096c-11eb-966d-954ab205cde1.jpg)

#### ViewModelOntwerp :    
![ViewModel_uml](https://user-images.githubusercontent.com/23512215/95454582-f36c2180-096c-11eb-9dba-fd104cf9cfa6.jpg)
