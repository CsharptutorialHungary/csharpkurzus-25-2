
# Sportszer (elsősorban sí és snowboard) kölcsönzőknek belső használatra készült alkalmazás

## Használat terminálból

* Sportszer felvitele a rendszerbe

```commandLine
addeq <path/to/newEquipmentData.json>
```

A newEquipmentData.json fájlnak az alábbi struktúrát kell követnie:

```JSON
{
  "Barcode": "<vonalkod>",
  "Sports": ["<sportag1>", "<sportag2>"],
  "Equipment type": "<sportszer tipusa>",
  "Brand": "<marka>",
  "Description": "<leiras>"
}
```

Vonalkód (Barcode) megadása kötelező, ez szolgál egyedi azonosítóként. Sportágat (Sports) legalább egyet meg kell adni, továbbá a sportszer típusát (Equipment type) is kötelező megadni. A márka (Brand) és a leírás (Description) megadása opcionális.

Példa:

```JSON
{
  "Barcode": "000123",
  "Sports": ["Skiing", "Snowboarding"],
  "Equipment type": "Ski helmet",
  "Brand": "Atomic",
  "Description": "size L"
}
```

* Sportszer adatainak módosítása

```commandLine
updeq <path/to/updatedEquipmentData.json>
```

Az updatedEquipmentData.json fájlban megadott vonalkód (Barcode) meg kell egyezzen a módosítandó sportszer vonalkódjával. Az összes adattag értéke felülírásra kerül az updatedEquipmentData.json-ben megadottakra.

* Sportszer törlése

```commandLine
deleq <Barcode>
```

A Barcode helyére a törlendő sportszer vonalkódját kell írni.

