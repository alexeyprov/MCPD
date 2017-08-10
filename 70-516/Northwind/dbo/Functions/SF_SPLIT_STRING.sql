-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[SF_SPLIT_STRING]
(	
	@PVI_STRING    VARCHAR(MAX),
	@PCI_DELIMITER CHAR(1)
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	WITH NUM_CTE(N)
	AS
	(
	    SELECT 1
	    
	    UNION ALL
	    
	    SELECT N + 1
	      FROM NUM_CTE
	     WHERE N < 10000
	)
    SELECT ROW_NUMBER() OVER (ORDER BY N) POSITION,
           LTRIM(RTRIM(ELEMENT)) ELEMENT
      FROM (SELECT N,
	               SUBSTRING(
	                   @PVI_STRING,
	                   N,
	                   CASE CHARINDEX(@PCI_DELIMITER, @PVI_STRING, N)
	                       WHEN 0 THEN LEN(@PVI_STRING) + 1 - N
	                       ELSE CHARINDEX(@PCI_DELIMITER, @PVI_STRING, N) - N
	                   END) ELEMENT
	          FROM NUM_CTE
	         WHERE N <= LEN(@PVI_STRING)
	           AND (N = 1 OR 
	                SUBSTRING(@PVI_STRING, N - 1, 1) = @PCI_DELIMITER)) Q
	 --WHERE Q.ELEMENT <> ''
)