# Notes:
* Assumption is made that the goal of this exercise is to showcase dotnet core ability therefore the database design was kept as simple as possible.
* Consideration should be taken to add below concerns:
	* ProductStock
	* Additional metadata for PurchaseOrder
	* Controller for PurchaseOrder

# Shopping Cart Concern 
## Get:
SELECT *
FROM ShoppingCartItem sci
INNER JOIN Product p ON p.Id = sci.ProductId
WHERE sci.UserId = @userId AND sci.PurchaseOrderId IS NULL;

## Get(@shoppingCartItemId):
SELECT *
FROM ShoppingCartItem sci
WHERE sci.UserId = @userId AND sci.Id = @shoppingCartItemId;

## Post:
INSERT INTO ShoppingCartItem
VALUES (@userId, @productId, @quantity, null, Now(), null);

## Post (/Checkout):
SELECT SUM(SellingPrice) INTO @totalCartSellingPrice
FROM ShoppingCartItem
WHERE UserId = @userId AND PurchaseOrderId IS NULL;

INSERT INTO PurchaseOrder
VALUES (@userId, Now(), @totalCartSellingPrice);

UPDATE ShoppingCartItem
SET PurchaseOrderId = @purchaseOrderId
WHERE @UserId = @userId AND PurchaseOrderId IS NULL;

## Put(@shoppingCartItemId):
UPDATE ShoppingCartItem
SET
  ProductId = @productId
  Quantity = @quantity
WHERE UserId = @userId AND Id = @shoppingCartItemId;

## Delete(@shoppingCartItemId):
DELETE FROM ShoppingCartItem
WHERE UserId = @userId AND Id = @shoppingCartItemId;

## Notes
* UserId is passed through every query to ensure a safety layer of security.
* UserId will be retrieved by using the access token of the request. If the token passes authentication, we can trust this.
* Discussion should happen regarding whether to move checkout to another controller or not.

# Product Concern 
## Get:
SELECT *
FROM Product;

## Get(@productId):
SELECT TOP 1
FROM Product
WHERE Id = @productId;

## Post:
INSERT INTO Product
VALUES (@name, @currentSellingPrice);

## Put(@productId):
UPDATE ProductId
SET
	Name = @name,
	CurrentSellingPrice = @currentSellingPrice
WHERE Id = @productId;

## Delete(@productId):
DELETE FROM ProductId
WHERE Id = @productId;

# ProductAttachment Concern
## Get(@productAttachmentId)
SELECT FilePath, FileName
FROM ProductAttachment
WHERE Id = @productAttachmentId;

-- Use the file service to get the file into a stream using above metadata and return it in the response

## Post(@productId)
-- @filePath => generate a file path using guid as the file name
-- @fileName => get the file name from the payload stream
-- Use the file service to save the file using above metadata

INSERT INTO ProductAttachment
VALUES (@productId, @filePath, @fileName);

## Delete(@productAttachmentId)
SELECT filePath into @filePath
FROM ProductAttachment
WHERE Id = @productAttachmentId;

-- Use the file service to delete the file using @filePath

DELETE FROM ProductAttachment
WHERE Id = @productAttachmentId;