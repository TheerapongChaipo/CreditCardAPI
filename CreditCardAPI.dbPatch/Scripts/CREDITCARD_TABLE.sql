USE [CreditcardDB]
GO

/****** Object:  Table [dbo].[CreditCard]    Script Date: 11/26/2018 12:01:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF  NOT EXISTS	(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreditCard]') AND type in (N'U'))
CREATE TABLE [dbo].[CreditCard](
	[CreditCardID] [int] IDENTITY(1,1) NOT NULL,
	[CardType] [nchar](50) NOT NULL,
	[CardNumber] [nchar](25) NOT NULL,
	[ExpMonth] [tinyint] NOT NULL,
	[ExpYear] [smallint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL CONSTRAINT [DF_CreditCard_ModifiedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_CreditCard] PRIMARY KEY CLUSTERED 
(
	[CreditCardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


