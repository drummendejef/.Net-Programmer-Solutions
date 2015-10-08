USE [SQLNET]
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

/*CREATE FUNCTION [dbo].[GetPositionsBasedOnX](@Xpos INT) */
ALTER FUNCTION [dbo].[GetPositionsBasedOnX](@Xpos [int]) 
 
RETURNS TABLE

RETURN 
    SELECT [id], 
           [xpos], 
           [ypos]
    FROM   [dbo].[positions] 
    WHERE  xpos = @Xpos

GO