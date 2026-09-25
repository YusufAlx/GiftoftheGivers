# Gift of the Givers — Custom Help Library

This project now includes a searchable, in-app Help Library at `/Help`.

## What was added

- `HelpLibrary/Articles/help-library.json` — editable knowledge base.
- `HelpLibrary/HelpArticle.cs` — article model.
- `HelpLibrary/HelpLibraryService.cs` — loads, searches, filters, and retrieves articles.
- `Controllers/HelpController.cs` — `/Help` and `/Help/Article/{id}` endpoints.
- `Views/Help/Index.cshtml` — searchable help home.
- `Views/Help/Article.cshtml` — individual article view.
- Navigation link in `Views/Shared/_Layout.cshtml`.
- Dependency registration in `Program.cs`.

## Adding an article

Edit `HelpLibrary/Articles/help-library.json` and add an object with:

- `Slug`: unique URL-safe identifier
- `Title`: article title
- `Category`: category shown in the UI
- `Tags`: search keywords
- `Summary`: short description
- `Content`: the full answer/guide

No database migration is required because the help library is content-based JSON.

## Included topics

Getting started, donor accounts, donations, anonymous donations, volunteering, employee access, employee dashboard, Azure donation function configuration, database behavior, local development, Azure deployment, donation troubleshooting, login troubleshooting, and production security.

## Important project-specific notes

The current application describes donations as a prototype and does not process real payments. The donation workflow depends on `AzureFunction:DonationUrl` and expects the function to return a `certificateNumber`. The seeded employee credentials are documented in the original README; change them before real deployment.
