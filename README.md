# SAP CDC OIDC Login/Logout Scenario App (C# / ASP.NET Core)

This sample app gives you pages to reproduce OIDC login/logout scenarios, including the **Standard User Logout Flow** from SAP Customer Data Cloud docs.

## What it demonstrates

- **Login page/action** that triggers an OIDC challenge (`/login`).
- **Standard logout flow** (`/logout/standard`) where the RP builds a request to SAP CDC `end_session_endpoint` using:
  - `id_token_hint` (current user ID token)
  - `client_id`
  - `postLogoutUrl`
- **RP-only logout** (`/logout/rp`) that only clears local cookie/session.
- **Trusted post logout landing page** (`/loggedout`) that displays the returned `mode` query parameter (for example `mode=consentLogout`).

## Configure

Update `appsettings.json` with real SAP CDC values:

- `SapCdcOidc:Authority`
- `SapCdcOidc:MetadataAddress`
- `SapCdcOidc:EndSessionEndpoint`
- `SapCdcOidc:ClientId`
- `SapCdcOidc:ClientSecret`
- `SapCdcOidc:TrustedPostLogoutUrl`

Ensure the `TrustedPostLogoutUrl` is configured in SAP CDC as a trusted post logout URL for your RP.

## Run

```bash
dotnet restore
dotnet run
```

Then open the app URL and use the buttons from the home page.
