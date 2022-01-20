# Agentensystem

# Design
![Results](https://github.com/IntelligentAgentsystems/2021actorsystems-2010454019/blob/master/Results/design.jpg)

## Komponenten
##### Operator
* Parent = UserGuardian
* Startet Spiele
* Restartet Spiele bei Timeout oder Exception
##### BrokerManager
* Parent = Operator
* Erstellt Exchange am externen RabbitMQ Broker
* Erstellt durable Queues für alle erstellten Spiele
##### Broker
* externer RabbitMQ MessageBroker
##### GameManager
* Parent = Operator
* Für 1 Spiel zuständig
* Fragt bestehende Spieldaten über BrokerCommunicator ab
* Erzeugt Playground
* Schickt nach jeder gespielten Runde die Resultate an den BrokerCommunicator
##### BrokerCommunicator
* Parent = GameManager
* Read/Write Kommunikation mit Broker für betreffendes Spiel
##### Playground
* Parent = GameManager
* Erzeugt Spieler von bestimmten Typ
* Fragt bei Spieler nach Tips und evauliert Ergebnisse
* Sendet Rundenergebnis an GameManager
##### Player1 & Player2
* Parent = Playground
* Verfolgen jeweils eine der folgenden Strategien
	* Cooperate
	* Defect
	* Gradual
	* Mistrust
	* Pavlov
	* Prober
	* Random
	* Spite
	* TitForTat

https://de.wikipedia.org/wiki/Gefangenendilemma#Strategien
## Entscheidungen
* Fokus - Resilienz -> Daten sind wichtig solange das Spiel läuft
	* Weiterspielen nach Fehler im Spiel oder Absturz der Applikation
* Ask<> Pattern beim Senden von Messages
* Tell<> Pattern bei Antworten
* Antworten senden immer Try<> der Code der Methode kapselt
* Bei Fehler im Spiel - GameManager (+Children) werden durch Operator gekillt.
	* Neustart des Spiels - neuer GameManager wird von Operator erzeugt
	* Fehlgeschlagene Spiele werden solange neugestartet bis Operator in Timeout läuft
* Timeout an 2 Stellen
	* System -> "Start Games" -> Operator
	* Operator -> "StartGame-X" -> GameManager
	* Keine weiteren Timeouts tiefer in der Hierachie um unnötige Komplexität zu vermeiden da bei Fehler ein gesamtes Spiel neu gestartet wird
* Externer MessageBroker (RabbitMQ) zum temporären Speichern von Daten (bis alle Spiele erfolgreich beendet sind)
	* Max 128 connections (TCP Socket bedingt)
### Mögliche Erweiterungen
* Broker Manager umbauen dass mehrere Broker initialisiert werden können
* Mehrere Broker laufen lassen um mehr als 128 Spiele gleichzeitig spielen zu können
* Kapselung von Playground & Player
	* bei Fehler in einer Spielrunde können Playground & Player neu gestartet werden
	* GameManager & BrokerCommunicator müssen weniger oft neu gestartet werden -> Skalierbarkeit da BrokerCommunicator immer Verbindung zum Broker aufbauen muss


# Ergebnisse

### Spielergebnisse - P1 vs P2
![Results](https://github.com/IntelligentAgentsystems/2021actorsystems-2010454019/blob/master/Results/ImgBest.png)
### Spielergebnisse - P1 + P2
![Results](https://github.com/IntelligentAgentsystems/2021actorsystems-2010454019/blob/master/Results/ImgBestTogether.png)
### Ausgabe 9 Spiele x 10 Runden
```yaml
a-d->AquireResources
a-c->AquireResources
a-a->AquireResources
a-e->AquireResources
a-f->AquireResources
a-b->AquireResources
a-g->AquireResources
a-h->AquireResources
a-i->AquireResources
a-h->Started
a-c->Started
a-e->Started
a-a-FAILED-Timeout after 00:00:01 seconds
a-b->Started
a-b-FAILED-Timeout after 00:00:01 seconds
a-c-FAILED-Timeout after 00:00:01 seconds
a-d-FAILED-Timeout after 00:00:01 seconds
a-e-FAILED-Timeout after 00:00:01 seconds
a-f-FAILED-Timeout after 00:00:01 seconds
a-g-FAILED-Timeout after 00:00:01 seconds
a-h-FAILED-Timeout after 00:00:01 seconds
a-i-FAILED-Timeout after 00:00:01 seconds


==========================================
a-a->AquireResources
a-b->AquireResources
a-c->AquireResources
a-d->AquireResources
a-e->AquireResources
a-f->AquireResources
a-g->AquireResources
a-h->AquireResources
a-a-FAILED-Timeout after 00:00:01 seconds
a-b-FAILED-Timeout after 00:00:01 seconds
a-c-FAILED-Timeout after 00:00:01 seconds
a-d-FAILED-Timeout after 00:00:01 seconds
a-e-FAILED-Timeout after 00:00:01 seconds
a-f-FAILED-Timeout after 00:00:01 seconds
a-g-FAILED-Timeout after 00:00:01 seconds
a-h-FAILED-Timeout after 00:00:01 seconds
a-i-FAILED-MayFail-Triggered
a-a->Started
a-e->Started
a-c->Started
a-b->Started
a-f->Started
a-d->Started
a-g->Started
a-h->Started


==========================================
a-a->AquireResources
a-b->AquireResources
a-d->AquireResources
a-e->AquireResources
a-f->AquireResources
a-h->AquireResources
a-i->AquireResources
a-c->AquireResources
a-g->AquireResources
a-b->Started
a-f->Started
a-h->Started
a-c->Started
a-d->Started
a-a->Started
a-e->Started
a-g->Started
a-i->Started
a-f->Round:0-Finished
a-h->Round:0-Finished
a-a->Round:0-Finished
a-d->Round:0-Finished
a-a->Round:1-Finished
a-d->Round:1-Finished
a-d->Round:2-Finished
a-d->Round:3-Finished
a-d->Round:4-Finished
a-d->Round:5-Finished
a-d->Round:6-Finished
a-a-FAILED-MayFail-Triggered
a-b-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-d-FAILED-MayFail-Triggered
a-e-FAILED-MayFail-Triggered
a-f-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered
a-h-FAILED-MayFail-Triggered
a-i-FAILED-MayFail-Triggered


==========================================
a-a->AquireResources
a-c->AquireResources
a-d->AquireResources
a-e->AquireResources
a-f->AquireResources
a-g->AquireResources
a-h->AquireResources
a-c->Started
a-g->Started
a-d->Started
a-e->Started
a-f->Started
a-a->Started
a-h->Started
a-h->Round:1-Finished
a-f->Round:1-Finished
a-h->Round:2-Finished
a-h->Round:3-Finished
a-a-FAILED-MayFail-Triggered
a-b-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-d-FAILED-MayFail-Triggered
a-e-FAILED-MayFail-Triggered
a-f-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered
a-h-FAILED-MayFail-Triggered
a-i-FAILED-MayFail-Triggered


==========================================
a-b->AquireResources
a-c->AquireResources
a-e->AquireResources
a-f->AquireResources
a-g->AquireResources
a-h->AquireResources
a-h->Started
a-f->Started
a-b->Started
a-g->Started
a-e->Started
a-c->Started
a-b->Round:0-Finished
a-f->Round:2-Finished
a-f->Round:3-Finished
a-f->Round:4-Finished
a-f->Round:5-Finished
a-f->Round:6-Finished
a-f->Round:7-Finished
a-f->Round:8-Finished
a-a-FAILED-MayFail-Triggered
a-b-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-d-FAILED-MayFail-Triggered
a-e-FAILED-MayFail-Triggered
a-f-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered
a-h-FAILED-MayFail-Triggered
a-i-FAILED-MayFail-Triggered


==========================================
a-a->AquireResources
a-b->AquireResources
a-c->AquireResources
a-e->AquireResources
a-d->AquireResources
a-f->AquireResources
a-g->AquireResources
a-h->AquireResources
a-i->AquireResources
a-h->Started
a-g->Started
a-d->Started
a-c->Started
a-i->Started
a-e->Started
a-b->Started
a-a->Started
a-f->Started
a-h->Round:4-Finished
a-c->Round:0-Finished
a-h->Round:5-Finished
a-e->Round:0-Finished
a-e->Round:1-Finished
a-i->Round:0-Finished
a-h->Round:6-Finished
a-e->Round:2-Finished
a-b->Round:1-Finished
a-h->Round:7-Finished
a-i->Round:1-Finished
a-i->Round:2-Finished
a-c->Round:1-Finished
a-d->Round:7-Finished
a-i->Round:3-Finished
a-d->Round:8-Finished
a-d->Round:9-Finished
a-a-FAILED-MayFail-Triggered
a-b-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-d-FINISHED
a-e-FAILED-MayFail-Triggered
a-f-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered
a-h-FAILED-MayFail-Triggered
a-i-FAILED-MayFail-Triggered


==========================================
a-a->AquireResources
a-b->AquireResources
a-e->AquireResources
a-f->AquireResources
a-g->AquireResources
a-h->AquireResources
a-a->Started
a-g->Started
a-h->Started
a-e->Started
a-f->Started
a-b->Started
a-a->Round:2-Finished
a-f->Round:9-Finished
a-a-FAILED-MayFail-Triggered
a-b-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-e-FAILED-MayFail-Triggered
a-f-FINISHED
a-g-FAILED-MayFail-Triggered
a-h-FAILED-MayFail-Triggered
a-i-FAILED-MayFail-Triggered


==========================================
a-a->AquireResources
a-e->AquireResources
a-g->AquireResources
a-h->AquireResources
a-i->AquireResources
a-g->Started
a-i->Started
a-a->Started
a-e->Started
a-g->Round:0-Finished
a-h->Started
a-a->Round:3-Finished
a-a->Round:4-Finished
a-e->Round:3-Finished
a-a->Round:5-Finished
a-e->Round:4-Finished
a-a->Round:6-Finished
a-e->Round:5-Finished
a-e->Round:6-Finished
a-e->Round:7-Finished
a-e->Round:8-Finished
a-h->Round:8-Finished
a-a-FAILED-MayFail-Triggered
a-b-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-e-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered
a-h-FAILED-MayFail-Triggered
a-i-FAILED-MayFail-Triggered


==========================================
a-a->AquireResources
a-b->AquireResources
a-c->AquireResources
a-e->AquireResources
a-g->AquireResources
a-h->AquireResources
a-i->AquireResources
a-e->Started
a-a->Started
a-h->Started
a-b->Started
a-i->Started
a-c->Started
a-g->Started
a-b->Round:2-Finished
a-i->Round:4-Finished
a-i->Round:5-Finished
a-i->Round:6-Finished
a-b->Round:3-Finished
a-h->Round:9-Finished
a-e->Round:9-Finished
a-a-FAILED-MayFail-Triggered
a-b-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-e-FINISHED
a-g-FAILED-MayFail-Triggered
a-h-FINISHED
a-i-FAILED-MayFail-Triggered


==========================================
a-a->AquireResources
a-b->AquireResources
a-c->AquireResources
a-i->AquireResources
a-i->Started
a-a->Started
a-b->Started
a-c->Started
a-c->Round:2-Finished
a-c->Round:3-Finished
a-c->Round:4-Finished
a-c->Round:5-Finished
a-a-FAILED-MayFail-Triggered
a-b-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered
a-i-FAILED-MayFail-Triggered


==========================================
a-a->AquireResources
a-b->AquireResources
a-c->AquireResources
a-g->AquireResources
a-i->AquireResources
a-c->Started
a-a->Started
a-b->Started
a-i->Started
a-g->Started
a-a->Round:7-Finished
a-i->Round:7-Finished
a-a-FAILED-MayFail-Triggered
a-b-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered
a-i-FAILED-MayFail-Triggered


==========================================
a-a->AquireResources
a-b->AquireResources
a-c->AquireResources
a-g->AquireResources
a-i->AquireResources
a-a->Started
a-g->Started
a-b->Started
a-i->Started
a-c->Started
a-g->Round:1-Finished
a-g->Round:2-Finished
a-g->Round:3-Finished
a-b->Round:4-Finished
a-b->Round:5-Finished
a-b->Round:6-Finished
a-b->Round:7-Finished
a-a->Round:8-Finished
a-b->Round:8-Finished
a-i->Round:8-Finished
a-b->Round:9-Finished
a-i->Round:9-Finished
a-a-FAILED-MayFail-Triggered
a-b-FINISHED
a-c-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered
a-i-FINISHED


==========================================
a-a->AquireResources
a-c->AquireResources
a-g->AquireResources
a-a->Started
a-c->Started
a-g->Started
a-g->Round:4-Finished
a-a-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered


==========================================
a-g->AquireResources
a-c->AquireResources
a-c->Started
a-g->Started
a-a-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered


==========================================
a-a->AquireResources
a-c->AquireResources
a-g->AquireResources
a-a->Started
a-g->Started
a-c->Started
a-g->Round:5-Finished
a-a-FAILED-MayFail-Triggered
a-c-FAILED-MayFail-Triggered
a-g-FAILED-MayFail-Triggered


==========================================
a-a->AquireResources
a-c->AquireResources
a-g->AquireResources
a-c->Started
a-a->Started
a-g->Started
a-c->Round:6-Finished
a-g->Round:6-Finished
a-c->Round:7-Finished
a-g->Round:7-Finished
a-g->Round:8-Finished
a-a->Round:9-Finished
a-g->Round:9-Finished
a-a-FINISHED
a-c-FAILED-MayFail-Triggered
a-g-FINISHED


==========================================
a-c->AquireResources
a-c->Started
a-c->Round:8-Finished
a-c->Round:9-Finished
a-c-FINISHED
```
