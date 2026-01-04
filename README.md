# Orc Email Sender

Windows desktop tool for sending batch HTML emails to Orcozol clients from a structured Excel (XLSX) list. Built as a multi-project .NET Framework solution with a clean separation of UI, business logic, data access, and DTO mapping.

## Highlights
- Batch send to up to 4 email addresses per client row with progress tracking and on-screen logging.
- Excel import driven by attribute-based column mapping (no hard-coded column names).
- SMTP configuration via `App.config`, keeping deployment-specific settings outside code.
- Layered architecture (WFApp + BLL + DAL + DTO) typical of enterprise WinForms systems.

## Tech Stack
- .NET Framework 4.5, C#
- Windows Forms UI
- SMTP via `System.Net.Mail`
- Excel integration via Interop (COM automation)
- Attribute-based DTO mapping for XLSX columns

## Architecture
- `OrcEMail.WFApp`: WinForms UI and workflow orchestration.
- `OrcEMail.BLL`: Business logic for importing the base and sending emails.
- `OrcEMail.DAL`: Excel and database-oriented utilities (generic CRUD + XLSX parsing).
- `OrcEMail.DTO`: DTOs and attribute metadata used by the DAL for mapping.

## Data Flow
1. User selects an XLSX file (see template below).
2. `BLL.BaseXLS` uses `DAL.XLSX` to read rows into `DTO.BaseXLS`.
3. For each row, the UI sends emails to `EMail1`..`EMail4`.
4. Progress and errors are shown in the form log and status bar.

## XLSX Template
The importer uses attribute metadata from `DTO.BaseXLS`:
- Row start: line 2, column A (see `XLSX_Planilha` attribute).
- Columns (in order):
  - `CPF` (A)
  - `EMail1` (B)
  - `EMail2` (C)
  - `EMail3` (D)
  - `EMail4` (E)

See `OrcEMail.WFApp/Modelo.xlsx` for the template.

## Configuration
SMTP settings live in `OrcEMail.WFApp/App.config`:
```xml
<appSettings>
  <add key="application.Dominio.Host" value="smtp.server.local" />
  <add key="application.Dominio.Usuario" value="user@domain.com" />
  <add key="application.Dominio.Senha" value="your-password" />
  <add key="application.Dominio.Port" value="25" />
</appSettings>
```

## Notable Implementation Details
- Attribute-driven XLSX parsing in `OrcEMail.DAL/XLSX.cs` with a reflection-based mapper.
- Generic DAL base (`OrcEMail.DAL/Base.cs`) designed for stored-procedure CRUD, typical of enterprise patterns.
- HTML editor control embedded in the UI for rich email content composition.
- User-friendly validations: required file, subject, and body checks before sending.

## Project Structure
```
OrcEMail.sln
OrcEMail.WFApp/   # WinForms app (UI)
OrcEMail.BLL/     # Business logic layer
OrcEMail.DAL/     # Data access and Excel integration
OrcEMail.DTO/     # DTOs and mapping attributes
```

## How to Run
1. Open `OrcEMail.sln` in Visual Studio (2013 or later).
2. Set `OrcEMail.WFApp` as the startup project.
3. Update SMTP settings in `OrcEMail.WFApp/App.config`.
4. Build and run, choose the XLSX file, write subject/body, click Enviar.

## Possible Improvements
- Move SMTP credentials to secure storage or environment variables.
- Add throttling and retry logic for large batches.
- Swap Excel Interop for a pure managed library to avoid COM dependencies.
- Add test coverage for XLSX parsing and email composition.
