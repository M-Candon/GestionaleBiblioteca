# 📚 Gestionale Biblioteca Full-Stack

Progetto per la gestione di libri e autori, sviluppato con **.NET 8 Web API** e **Angular Standalone**.

## 🏗️ Struttura del Progetto

Il repository è organizzato in due moduli principali:
- **`GestionaleBiblioteca.Server`**: Backend ASP.NET Core con database SQLite.
- **`GestionaleBiblioteca-UI`**: Frontend Angular con Bootstrap e icone Bootstrap.

---

## 🚀 Come avviare il progetto

### 1. Backend (C# / .NET)
1. Aprire la cartella `GestionaleBiblioteca.Server` o il file della Solution (`.slnx` / `.sln`) con **Visual Studio**.
2. Assicurati che il progetto `Server` sia impostato come progetto di avvio.
3. Premi **F5** (o il tasto Play) per avviare l'applicazione.
   - *Nota:* Al primo avvio, il database SQLite verrà creato automaticamente e popolato con dati di test (Seed data).

### 2. Frontend (Angular)
Il frontend richiede l'installazione di Node.js e Angular CLI.

1. Apri un terminale o il prompt dei comandi nella cartella `GestionaleBiblioteca-UI`.
2. Se è la prima volta che scarichi il progetto, installa le dipendenze:
   ```bash
   npm install
3. Avvia il server di sviluppo con il comando per ignorare i problemi di certificato SSL locale:
   ```bash
   set NODE_TLS_REJECT_UNAUTHORIZED=0 && ng serve
4. Apri il browser all'indirizzo http://localhost:4200

## 🛠️ Funzionalità implementate
- [x] CRUD Libri: Gestione completa (Inserimento, Visualizzazione, Modifica, Eliminazione).
- [x] CRUD Autori: Gestione anagrafica autori.
- [x] Ricerca Dinamica: Filtro istantaneo per titolo libro o genere.
- [x] Database Seed: Generazione automatica di dati all'avvio del backend.
- [x] UI Responsive: Interfaccia pulita realizzata con Bootstrap.

## 📝 Note Tecniche
Il comando NODE_TLS_REJECT_UNAUTHORIZED=0 è necessario per permettere ad Angular di comunicare con il backend via HTTPS quando si usano certificati di sviluppo auto-firmati.
