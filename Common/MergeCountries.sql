SELECT
	n.Id,
	Parent,
	[Caption],
	CurrencyUnit,
	[ISO 3166 numeric code],
	[ISO 3166 alpha2 code],
	[ISO 3166 alpha3 code],
	[WDI code],
	[WO code]
FROM 
(SELECT
	*
	FROM [Elements] LEFT JOIN [Attributes] ON
		[Elements].[Id] = [Attributes].[ElementId] AND
		[Elements].[ParentId] = [Attributes].[ElementParentId] AND
		[Elements].[DimensionId] = [Attributes].[ElementDimensionId]
		PIVOT (MAX(Value) FOR [Attributes].[Id] IN (
			[Caption],
			[ISO 3166 alpha2 code],
			[ISO 3166 alpha3 code],
			[ISO 3166 numeric code],
			[WDI code],
			[WO code]
			)) AS pvt
	WHERE [DimensionId] = 'Country') AS n
	LEFT JOIN [WDI].[dbo].[Countries] AS c 
	ON
	[n].[WDI Code] = [c].[Id] 