/*Catalog for Users*/
CREATE FULLTEXT CATALOG UsersCatalog AS DEFAULT;
CREATE FULLTEXT INDEX ON [ApplicationUsers]([UserName], [FirstName], [LastName]) KEY INDEX PK_AspNetUsers;

/*Catalog for Posts*/
CREATE FULLTEXT CATALOG PostsCatalog AS DEFAULT;
CREATE FULLTEXT INDEX ON [Posts]([Title], [Content], [ShortContent]) KEY INDEX PK_Posts;

/*Catalog for Recommended Users*/
CREATE FULLTEXT CATALOG RecommendedUsersCatalog AS DEFAULT;
CREATE FULLTEXT INDEX ON [RecommendedFriends]([RecommendedFirstName], [RecommendedLastName], [RecommendedUsername]) KEY INDEX PK_RecommendedFriends;
