- Report to show all authors and array of article ids written by them

WITH AuthorArticles AS
(
    SELECT 
        u.Id AS AuthorId,
        u.Username AS Author,
        c.ArticleId
    FROM Users u
    Left JOIN Contents c
        ON u.Id = c.AuthorId
)
SELECT
    AuthorId,
	Author,
    STRING_AGG(CAST(ArticleId AS VARCHAR(10)), ', ') AS ArticleIds
FROM AuthorArticles
GROUP BY
    AuthorId,
	Author;



-Report to show all articles created in past 3 months, by users created in the past 4 months with specified language 


DECLARE @Language INT = 1;              -- 1 = English, 2 = French, 3 = Spanish
DECLARE @ArticleMonths INT = 3;         -- articles created in past 3 months
DECLARE @UserMonths INT = 4;            -- users created in past 4 months

SELECT 
    a.Id AS ArticleId,
    c.Title,
    u.Id AS AuthorId,
    u.Username,
    c.Language,
    a.Status,
    a.CreatedAt AS ArticleCreatedAt,
    u.CreatedAt AS UserCreatedAt
FROM Articles a
JOIN Contents c ON c.ArticleId = a.Id
JOIN Users u ON u.Id = c.AuthorId
WHERE c.Language = @Language
  AND a.CreatedAt >= DATEADD(MONTH, - @ArticleMonths, GETDATE())
  AND u.CreatedAt >= DATEADD(MONTH, - @UserMonths, GETDATE())
ORDER BY a.CreatedAt DESC;
