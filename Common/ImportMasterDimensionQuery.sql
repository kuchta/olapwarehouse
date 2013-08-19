--DECLARE @columns NVARCHAR(2000)

--SELECT @columns = Coalesce(@columns + ', [', '[') + Name + ']' FROM (
--	SELECT DISTINCT a.Name
--	FROM [Attributes] a LEFT JOIN [Elements] e ON a.[ElementId] = e.[Id] LEFT JOIN [Dimensions] d ON e.[DimensionId] = d.[Id]
--	WHERE d.[Name] = 'Country'
--) s

--DECLARE @query NVARCHAR(4000) 
--SET @query = '
--SELECT
--	[ParentId]
--	,[ElementId]
--	,[Caption]
--	,' + @columns + '
--FROM (
--	SELECT d.Name AS DimensionId, e2.Name AS ParentId, e.Name AS ElementId, e.Caption, a.Name as AttributeName, a.Value as AttributeValue
--	FROM [Attributes] a LEFT JOIN [Elements] e ON a.[ElementId] = e.[Id] LEFT JOIN [Elements] e2 ON e.[ParentId] = e2.[Id] LEFT JOIN [Dimensions] d ON e.[DimensionId] = d.[Id]
--	WHERE d.[Name] = ''Country''
--	) data
--PIVOT (MAX(AttributeValue) FOR AttributeName IN (' + @columns + ')) as pvt'

--EXECUTE(@query)


DECLARE @columns NVARCHAR(2000)

SELECT @columns = Coalesce(@columns + ', [', '[') + Name + ']' FROM (
	SELECT DISTINCT a.Name
	FROM [Attributes] a LEFT JOIN [Elements] e ON a.[ElementId] = e.[Id] LEFT JOIN [Dimensions] d ON e.[DimensionId] = d.[Id]
	WHERE d.[Name] = 'Country'
) s

DECLARE @query NVARCHAR(4000) 
SET @query = '
SELECT
	[ParentId]
	,[ElementId]
	,[Caption]
	,' + @columns + '
FROM (
	SELECT d.Name AS DimensionId, e2.Name AS ParentId, e.Name AS ElementId, e.Caption, a.Name as AttributeName, a.Value as AttributeValue
	FROM [Attributes] a LEFT JOIN [Elements] e ON a.[ElementId] = e.[Id] LEFT JOIN [Elements] e2 ON e.[ParentId] = e2.[Id] LEFT JOIN [Dimensions] d ON e.[DimensionId] = d.[Id]
	WHERE d.[Name] = ''Country''
	) data
PIVOT (MAX(AttributeValue) FOR AttributeName IN (' + @columns + ')) as pvt'

EXECUTE(@query)
