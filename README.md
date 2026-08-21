# Manuela Ruiz Realty

Website comercial para Manuela Ruiz (Guerra & Associates Realty LLC), basado en el prototipo ChatGPT Sites.

Stack: **ASP.NET Core 8 MVC + Razor + CSS + JavaScript** — mismo enfoque que `safe-project`. El prototipo queda como especificación visual; no es la base de producción.

Por agilidad, **no hay base de datos**. Los leads de formularios viven en memoria (`LeadStore`) y se ven en `/ops`.

## Correr

Requisito: .NET 8 SDK.

```bash
cd /Users/mac/Projects/manuela-ruiz-realty
dotnet run --project src/ManuelaRuiz.Web --urls http://localhost:5128
```

Abre `http://localhost:5128`.

## Demo 1 (sandbox)

1. Home alineada al prototipo (zero-down, hero, búsqueda, intake, calculadoras, portafolio, ciudades, programas, partners)
2. Páginas: `/search`, `/qualify`, `/buyers`, `/sellers`, `/relocation`, `/areas`, `/programs`, `/partners`, `/sold`, `/first-time-buyers`, `/investors`, `/new-construction`, `/resources`
3. Idioma ES/EN con cookie `mr.lang` (español por defecto)
4. Formularios → `LeadStore` in-memory → tablero `/ops`
5. Calculadora hipotecaria y estimador de poder de compra en el navegador

**No activado:** CRM, email transaccional, MLS live, credit pull, ni solicitud de préstamo real.

## Publicar (gratis en Render)

1. Repo en GitHub: `CarlosCortes641/manuela-ruiz-realty`
2. En [Render](https://dashboard.render.com/blueprint/new?repo=https://github.com/CarlosCortes641/manuela-ruiz-realty): Blueprint Name `manuela-ruiz-realty` → **Apply**
3. Si falla por memoria (exit 139): en el servicio → **Manual Deploy** → **Clear build cache & deploy**. El runtime usa Debian slim (no Alpine) para evitar segfaults musl en el plan free.

El plan free se duerme sin tráfico; el primer request puede tardar ~30–60s.

## Prototipo de referencia

https://manuela-ruiz-realty.jrricardo29.chatgpt.site
