SELECT ElementId1 AS Country,
               ElementId2 AS Indicator,
               ElementId3 AS Year
FROM Facts f
LEFT JOIN Cubes c ON f.CubeId = c.Id
LEFT JOIN Databases d ON c.DatabaseId = d.Id
WHERE d.Name = 'WDI' AND c.Name = 'WDI'