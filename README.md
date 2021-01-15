# Handelsplattform

Auflistung der Microservices:
- Blackfriday
- IEGEasyCreditCardRobinService
- IEGEasyCreditCardService
- Logging
- PaymentService
- ProductCatalog
- ProductCatalogFTP

Aufgabe 2:
Für diese Aufgabe wurden die Microservices ProductCatalog und ProductCatalogFTP erstellt. Ersteres dient als dezentralisierter Speicherort der Produkte.
Über definierte Endpunkte (Url) wird auf diese Liste an Produkten zugegriffen.

Weiteres ist über den ProductCatalogFTP die Liste auf einem FTP-Server erreichbar. Dort ist ein txt-File mit einer Liste an Produkten gespeichert, die im Microservice 
ausgelesen wird.


Aufgabe 3:
Für diese Aufgabe wurde der Microservice IEGEasyCreditCardRobinService erstellt. Der Microservice wird aufgerufen und direkt in eine for-Schleife integriert. 
Wenn der Aufruf scheitert, wird 1 Sekunde gewartet und neu versucht. Dies dient als "Design for failure", da hier automatisch auf eventuelle Fehler reagiert wird.
Mithilfe eines Breakpoints kann der Service live getestet werden und man kann im Code den Durchlauf in der Schleife nachbilden.
Weiteres wurde hierfür ein Loggingservice erstellt, der den Ist-Zustand der Services mitprotokolliert und auftretende Fehlermeldungen mittrackt.

Aufgabe 5:
Für diese Aufgabe wurde der Microservice PaymentService erstellt. Dieser Service bietet die Möglichkeit verschiedene Nachrichtenformate zu bearbeiten. 
Beispiele hierfür wären CSV, xml und JSON files. Der Typ wird im Contentheader definiert und so kann der Microservice den entsprechenden Typ bearbeiten.