-- ajout utilisateur 
CREATE USER "C#ROOT"@"localhost" identified by "9521094370";
-- donner les droits sur les bdd ski et tt les tables
GRANT ALL ON Cookingest.* TO "C#ROOT"@"localhost";

-- DB
DROP DATABASE IF EXISTS Cookingest;
CREATE DATABASE Cookingest;
USE Cookingest;

# CREATION TABLES
CREATE TABLE Clients(
	clientID int AUTO_INCREMENT PRIMARY KEY,
	mail VARCHAR(100) NOT NULL UNIQUE,
	motDePasse VARCHAR(255) NOT NULL,
	nomClient VARCHAR(30) NOT NULL,
	prenomClient VARCHAR(30) NOT NULL,
	telephoneClient DECIMAL(10,0) NOT NULL,
	soldeBanque INT DEFAULT 1000 CHECK (soldeBanque >= 0),
	adresseClient varchar(100),
    
    estAdmin BOOL DEFAULT FALSE
);

CREATE TABLE Createurs(
	createurID INT AUTO_INCREMENT PRIMARY KEY,
	clientID INT, 
	FOREIGN KEY (clientID) REFERENCES Clients(clientID),
	soldePoint INT DEFAULT 0
   -- pas de idRecette dans Createur car un createur peut avoir plusieurs recette
);

CREATE TABLE Recettes(
	recetteID INT AUTO_INCREMENT PRIMARY KEY,
	createurID INT,
	
	nomRecette VARCHAR(30),
	descriptif VARCHAR(255),

	dateRecette DATETIME DEFAULT CURRENT_TIMESTAMP NOT NUll,
	-- prix de la recette automatiquement calcule via le prix de ses ingredients
    
    FOREIGN KEY (createurID) REFERENCES Createurs(createurID)
   -- recompense est calcul dynamiquement en fonction du prix
);

CREATE TABLE IngredientCategorie(
	categorieID INT AUTO_INCREMENT PRIMARY KEY,
    
	nomCategorie VARCHAR(30)
);

CREATE TABLE Fournisseur(
	fournisseurID INT AUTO_INCREMENT PRIMARY KEY,
	nomFournisseur VARCHAR(40),
	num_siret DECIMAL(12,0),
	adresseFournisseur VARCHAR(100),
	telephoneFournisseur DECIMAL(10,0)
);
-- doublon ingredient et produit
CREATE TABLE Ingredient(
	ingredientID INT AUTO_INCREMENT PRIMARY KEY,
    fournisseurID INT NOT NULL,
    
    categorieID INT,
	FOREIGN KEY (categorieID) REFERENCES IngredientCategorie(categorieID),
	nomIngredient VARCHAR(30),
	descriptif VARCHAR(255),
	prixIngredient INT,
	stock INT,
	stockMin INT,
	stockMax INT,
	
	FOREIGN KEY (fournisseurID) REFERENCES Fournisseur(fournisseurID)
);

CREATE TABLE IngredientRecette(
	ingredientID INT,
	recetteID INT,
	FOREIGN KEY (ingredientID) REFERENCES Ingredient(ingredientID),
	FOREIGN KEY (recetteID) REFERENCES Recettes(recetteID)
);

CREATE TABLE HistoriqueCommandes(
	commandeID INT AUTO_INCREMENT PRIMARY KEY,
	recetteID INT,
	clientID INT, -- le client qui a commandé
    
    prix INT, -- pour les commandes via solde createur le solde createur est d'abord convertit en € avant d'executer le processus de commande
    
	dateCommande DATETIME DEFAULT CURRENT_TIMESTAMP NOT NUll,
    
    
	FOREIGN KEY (recetteID) REFERENCES Recettes(recetteID),
	FOREIGN KEY (clientID) REFERENCES Clients(clientID)
);

# .--------------.
# |  PEUPLEMENT  |
# .--------------.

# Fournisseur
INSERT INTO Fournisseur (nomFournisseur, num_siret, adresseFournisseur, telephoneFournisseur) VALUES 
("ATP Meal", 123456789012, "123 rue des aliments", 0123456789),
("Elior", 234567890123, "456 avenue des repas", 1234567890),
("AS Static", 345678901234, "789 boulevard des menus", 2345678901),
("O'Cosymeal", 456789012345, "12 rue des plats", 3456789012),
("Macmil ranch", 567890123456, "45 chemin des viandes", 4567890123);

# Catgeorie Ingredient
INSERT INTO IngredientCategorie (categorieID, nomCategorie) VALUES (1, "viande");
INSERT INTO IngredientCategorie (categorieID, nomCategorie) VALUES (2, "Fromage");
INSERT INTO IngredientCategorie (categorieID, nomCategorie) VALUES (3, "Pain");
INSERT INTO IngredientCategorie (categorieID, nomCategorie) VALUES (4, "Cruditée");
INSERT INTO IngredientCategorie (categorieID, nomCategorie) VALUES (6, "Sauce");

# Ingredient


-- Fromages 
INSERT INTO Ingredient (nomIngredient, categorieID, descriptif, prixIngredient, stock, stockMin, stockMax,fournisseurID) 
VALUES 
('Fromage bleu', 2, 'Fromage bleu affiné avec un goût fort et piquant', 1.50, FLOOR(RAND() * 200), 0, 200,1),--
('Fromage de chèvre', 2, 'Fromage de chèvre frais et crémeux', 1.75, FLOOR(RAND() * 200), 0, 200,3),--
('Mozzarella', 2, 'Fromage mozzarella doux et fondant', 1.25, FLOOR(RAND() * 200), 0, 200,2),--
('Feta', 2, 'Fromage feta salé et acidulé', 1.50, FLOOR(RAND() * 200), 0, 200,1),--
('Fromage de brebis', 2, 'Fromage de brebis riche et crémeux', 2.00, FLOOR(RAND() * 200), 0, 200,1),--
('Fromage de raclette', 2, 'Fromage de raclette fondu et savoureux', 2.50, FLOOR(RAND() * 200), 0, 200,2),--
('Fromage de brie', 2, 'Fromage de brie doux et crémeux', 2.00, FLOOR(RAND() * 200), 0, 200,4),--
('Cheddar', 2, 'Fromage cheddar fort et savoureux', 1.75, FLOOR(RAND() * 200), 0, 200,1),--
('Parmesan', 2, 'Fromage parmesan râpé et salé', 2.50, FLOOR(RAND() * 200), 0, 200,3);--
-- Pains
INSERT INTO Ingredient (nomIngredient, categorieID, descriptif, prixIngredient, stock, stockMin, stockMax,fournisseurID) 
VALUES 
('Pain blanc', 3, 'Pain blanc moelleux', 0.50, FLOOR(RAND() * 200), 0, 200,1),--
('Pain complet', 3, 'Pain complet riche en fibres', 0.75, FLOOR(RAND() * 200), 0, 200,2),--
('Pain de seigle', 3, 'Pain de seigle dense et rustique', 1.25, FLOOR(RAND() * 200), 0, 200,4),--
('Pain aux noix', 3, 'Pain aux noix croquant et savoureux', 1.50, FLOOR(RAND() * 200), 0, 200,3),--
('Pain de mie', 3, 'Pain de mie moelleux et léger', 0.75, FLOOR(RAND() * 200), 0, 200,1),--
('Pain de maïs', 3, 'Pain de maïs sucré et croustillant', 1.00, FLOOR(RAND() * 200), 0, 200,4),--
('Baguette', 3, 'Baguette croustillante et légère', 1.50, FLOOR(RAND() * 200), 0, 200,2),--
('Pain pita', 3, 'Pain pita moelleux et léger', 1.25, FLOOR(RAND() * 200), 0, 200,3),--
('Pain ciabatta', 3, 'Pain ciabatta croustillant et savoureux', 1.75, FLOOR(RAND() * 200), 0, 200,1);--
-- Cruditées
INSERT INTO Ingredient (nomIngredient, categorieID, descriptif, prixIngredient, stock, stockMin, stockMax,fournisseurID) 
VALUES 
('Tomate cerise', 4, 'Petites tomates rouges et sucrées', 0.50, FLOOR(RAND() * 200), 0, 200,3),--
('Tomate coeur de boeuf', 4, 'Tomates charnues et juteuses', 1.25, FLOOR(RAND() * 200), 0, 200,4),--
('Tomate roma', 4, 'Tomates allongées et savoureuses', 1.00, FLOOR(RAND() * 200), 0, 200,1),--
('Tomate ananas', 4, 'Tomates jaunes et douces', 1.50, FLOOR(RAND() * 200), 0, 200,2),--
('Roquette', 4, 'Salade de roquette fraîche et piquante', 1.50, FLOOR(RAND() * 200), 0, 200,1),--
('Laitue', 4, 'Salade de laitue croquante et légère', 1.00, FLOOR(RAND() * 200), 0, 200,4),--
('Mâche', 4, 'Salade de mâche douce et veloutée', 1.25, FLOOR(RAND() * 200), 0, 200,3),--
('Endive', 4, 'Salade d\'endives croquantes et légèrement amères', 1.50, FLOOR(RAND() * 200), 0, 200,4),--
('Oignon rouge', 4, 'Oignon rouge doux et sucré', 0.75, FLOOR(RAND() * 200), 0, 200,2),--
('Oignon jaune', 4, 'Oignon jaune fort et savoureux', 0.50, FLOOR(RAND() * 200), 0, 200,1),--
('Oignon vert', 4, 'Oignon vert doux et croquant', 0.75, FLOOR(RAND() * 200), 0, 200,4),--
('Oignon doux', 4, 'Oignon doux et légèrement sucré', 1.00, FLOOR(RAND() * 200), 0, 200,3),--
('Maïs', 4, 'Maïs doux et croquant', 1.00, FLOOR(RAND() * 200), 0, 200, 1),
('Haricots rouges', 4, 'Haricots rouges tendres et savoureux', 2.00, FLOOR(RAND() * 200), 0, 200, 2),
('Avocat', 4, 'Avocat crémeux et savoureux', 3.00, FLOOR(RAND() * 200), 0, 200, 3),
('Lentilles', 4, 'Lentilles tendres et riches en protéines', 1.50, FLOOR(RAND() * 200), 0, 200, 4),
('Céleri', 4, 'Céleri croquant et rafraîchissant', 0.75, FLOOR(RAND() * 200), 0, 200, 5),
('Betteraves', 4, 'Betteraves sucrées et colorées', 1.25, FLOOR(RAND() * 200), 0, 200, 1),
('Endive', 4, 'Endive croquante et légèrement amère', 1.00, FLOOR(RAND() * 200), 0, 200, 2),
('Carotte râpée', 4, 'Carotte râpée fraîche et croquante', 1.50, FLOOR(RAND() * 200), 10, 200, 3),
('Chou rouge', 4, 'Chou rouge croquant et savoureux', 1.25, FLOOR(RAND() * 200), 0, 200, 4),
('Chou blanc', 4, 'Chou blanc croquant et léger', 1.00, FLOOR(RAND() * 200), 0, 200, 5),
('Fenouil', 4, 'Fenouil frais et anisé', 2.00, FLOOR(RAND() * 200), 0, 200, 1);

-- Sauces
insert into Ingredient (nomIngredient, categorieID, descriptif, prixIngredient, stock, stockMin, stockMax,fournisseurID)
VALUES 
('Moutarde', 6, 'Moutarde forte et savoureuse', 1.00, FLOOR(RAND() * 10), 0, 10, 1),
('Ketchup', 6, 'Ketchup sucré et délicieux', 1.50, FLOOR(RAND() * 10), 0, 10, 2),
('Mayonnaise', 6, 'Mayonnaise crémeuse et onctueuse', 2.00, FLOOR(RAND() * 10), 0, 10, 3),
('Sauce barbecue', 6, 'Sauce barbecue fumée et savoureuse', 2.50, FLOOR(RAND() * 10), 0, 10, 4),
('Sauce piquante', 6, 'Sauce piquante pour les amateurs de sensations fortes', 1.75, FLOOR(RAND() * 10), 0, 10, 5),
('Sauce soja', 6, 'Sauce soja salée et umami', 2.50, FLOOR(RAND() * 10), 0, 10, 1),
('Sauce chili', 6, 'Sauce chili épicée et savoureuse', 1.50, FLOOR(RAND() * 10), 0, 10, 2),
('Vinaigre balsamique', 6, 'Vinaigre balsamique doux et acidulé', 3.00, FLOOR(RAND() * 10), 0, 10, 3),
('Sauce curry', 6, 'Sauce curry épicée, idéale pour accompagner du poulet ou du riz', 4.00, FLOOR(RAND() * 10), 0, 10, 1),
('Sauce miel moutarde', 6, 'Sauce miel moutarde sucrée et épicée, idéale pour accompagner du porc ou du poulet', 5.00, FLOOR(RAND() * 10), 0, 10, 2),
('Sauce samouraï', 6, 'Sauce samouraï épicée, idéale pour accompagner des viandes grillées ou des frites', 3.00, FLOOR(RAND() * 10), 0, 10, 3),
('Sauce algérienne', 6, 'Sauce algérienne piquante, idéale pour accompagner des plats de viande ou de poisson', 4.50, FLOOR(RAND() * 10), 0, 10, 4),
('Sauce biggy', 6, 'Sauce biggy sucrée et épicée, idéale pour accompagner des burgers ou des hot-dogs', 2.50, FLOOR(RAND() * 10), 0, 10, 1),
('Oignons doux', 6, 'Oignons doux caramélisés, idéaux pour accompagner des viandes grillées ou des plats de pâtes', 3.50, FLOOR(RAND() * 10), 0, 10, 1),
('Sauce tartare', 6, 'Sauce tartare crémeuse, idéale pour accompagner des fruits de mer ou des frites', 4.00, FLOOR(RAND() * 10), 0, 10,1),
('Sauce ranch', 6, 'Sauce ranch crémeuse, idéale pour accompagner des légumes ou des viandes grillées', 3.00, FLOOR(RAND() * 10), 0, 10, 1),
('Sauce blanche', 6, 'Sauce blanche crémeuse, idéale pour accompagner des kebabs ou des falafels', 2.50, FLOOR(RAND() * 10), 0, 10, 1),
('Sauce harissa', 6, 'Sauce harissa épicée, idéale pour accompagner des kebabs ou des grillades', 3.50, FLOOR(RAND() * 10), 0, 10,2),
('Sauce algérienne', 6, 'Sauce algérienne épicée, idéale pour accompagner des kebabs ou des sandwichs', 4.00, FLOOR(RAND() * 10), 0, 10, 1),
('Sauce yogurt', 6, 'Sauce yogurt crémeuse, idéale pour accompagner des gyros ou des kebabs', 2.50, FLOOR(RAND() * 10), 0, 10, 2),
('Sauce tzatziki', 6, 'Sauce tzatziki crémeuse, idéale pour accompagner des gyros ou des kebabs', 4.50, FLOOR(RAND() * 10), 0, 10, 3);

-- Viandes
INSERT INTO Ingredient (nomIngredient, categorieID, descriptif, prixIngredient, stock, stockMin, stockMax,fournisseurID)
VALUES 
('Poulet caramel', 1, 'Poulet cuit dans une sauce sucrée et caramélisée', 4.50, FLOOR(RAND() * 100), 0, 200, 1),
('Poulet frit', 1, 'Poulet pané et frit pour un croustillant irrésistible', 5.00, FLOOR(RAND() * 100), 0, 200, 2),
('Poulet mariné', 1, 'Poulet mariné dans des épices savoureuses', 4.75, FLOOR(RAND() * 100), 0, 200, 3),
('Poulet curry', 1, 'Poulet cuit dans une sauce épicée et crémeuse au curry', 5.50, FLOOR(RAND() * 100), 0, 200, 4),
('Poulet tikka', 1, 'Poulet mariné dans une sauce épicée et cuits au four tandoor', 6.00, FLOOR(RAND() * 100), 0, 200, 5),
('Boeuf frit', 1, 'Boeuf pané et frit pour un croustillant irrésistible', 8.00, FLOOR(RAND() * 200), 0, 200, 1),
('Boeuf de Kobe', 1, 'Boeuf de Kobe, une viande japonaise haut de gamme', 20.00, FLOOR(RAND() * 200), 0, 200, 2),
('Boeuf Wagiu', 1, 'Boeuf Wagiu, une viande japonaise riche en graisse et en saveurs', 15.00, FLOOR(RAND() * 200), 0, 200, 3),
('Boeuf charentais', 1, 'Boeuf charentais, une viande française tendre et savoureuse', 12.00, FLOOR(RAND() * 200), 0, 200, 4),
('Boeuf bourguignon', 1, 'Boeuf mijoté avec des légumes et du vin rouge', 10.00, FLOOR(RAND() * 200), 0, 200, 5),
('Bavette d aloyau', 1, 'Bavette de boeuf tendre et juteuse', 12.00, FLOOR(RAND() * 200), 0, 200, 1),
('Tournedos Rossini', 1, 'Tournedos de boeuf surmonté de foie gras poêlé et d une sauce truffée', 25.00, FLOOR(RAND() * 200), 0, 200, 2),
('Entrecôte grillée', 1, 'Entrecôte de boeuf grillée, juteuse et savoureuse', 18.00, FLOOR(RAND() * 200), 0, 200, 3),
('Filet de boeuf Wellington', 1, 'Filet de boeuf enrobé de pâte feuilletée et cuit au four', 30.00, FLOOR(RAND() * 200), 0, 200, 4),
('Bacon', 1, 'Bacon fumé et croustillant', 6.00, FLOOR(RAND() * 200), 0, 200, 1),
('Salami', 1, 'Salami italien sec et épicé', 8.00, FLOOR(RAND() * 200), 0, 200, 2),
('Travers de porc', 1, 'Travers de porc grillés et caramélisés', 12.00, FLOOR(RAND() * 200), 0, 200, 3),
('Jambon de parme', 1, 'Jambon de parme italien finement tranché', 15.00, FLOOR(RAND() * 200), 0, 200, 4),
('Saucisse de Toulouse', 1, 'Saucisse de porc de Toulouse, fumée et épicée', 10.00, FLOOR(RAND() * 200), 0, 200, 5),
('Côte de porc grillée', 1, 'Côte de porc grillée, juteuse et savoureuse', 14.00, FLOOR(RAND() * 200), 0, 200, 1),
('Ribs de porc', 1, 'Ribs de porc grillés et enrobés de sauce barbecue', 16.00, FLOOR(RAND() * 200), 0, 200, 2),
('Jambon fumé', 1, 'Jambon de porc fumé et tranché', 12.00, FLOOR(RAND() * 200), 0, 200, 3),
('Saucisson sec', 1, 'Saucisson sec de porc, séché et épicé', 9.00, FLOOR(RAND() * 200), 0, 200, 4),
('Tranches de saumon', 1, 'Tranches de saumon frais, prêtes à être grillées ou poêlées', 15.00, FLOOR(RAND() * 100), 0, 200, 1),
('Pavé de saumon', 1, 'Pavé de saumon frais, idéal pour une cuisson au four', 18.00, FLOOR(RAND() * 100), 0, 200, 2),
('Thon à l’huile', 1, 'Thon en conserve à l’huile, parfait pour une salade ou une pizza', 6.00, FLOOR(RAND() * 100), 0, 200, 3),
('Thon rouge', 1, 'Thon rouge frais, idéal pour une cuisson au grill ou à la poêle', 20.00, FLOOR(RAND() * 100), 0, 200, 4),
('Carpaccio de thon', 1, 'Carpaccio de thon frais, finement tranché et prêt à être dégusté', 25.00, FLOOR(RAND() * 100), 0, 200, 5),
('Thon snacké', 1, 'Thon snacké croustillant, idéal pour une entrée ou un apéritif', 12.00, FLOOR(RAND() * 100), 0, 200, 3),
('Filet de cabillaud', 1, 'Filet de cabillaud frais, idéal pour une cuisson au four ou à la poêle', 14.00, FLOOR(RAND() * 100), 0, 200, 4),
('Moules de bouchot', 1, 'Moules de bouchot fraîches, prêtes à être cuisinées en sauce', 10.00, FLOOR(RAND() * 100), 0, 200, 1),
('Noix de Saint-Jacques', 1, 'Noix de Saint-Jacques fraîches, idéales pour une cuisson à la poêle ou au grill', 30.00, FLOOR(RAND() * 100), 0, 200, 2),
('Filet de truite', 1, 'Filet de truite frais, idéal pour une cuisson au four ou à la poêle', 16.00, FLOOR(RAND() * 100), 0, 200, 5),
('Crevettes roses', 1, 'Crevettes roses fraîches, idéales pour une cuisson à la poêle ou sur le grill', 20.00, FLOOR(RAND() * 100), 0, 200, 1),
('Poulpe', 1, 'Poulpe frais, prêt à être cuisiné en salade ou en plat chaud', 25.00, FLOOR(RAND() * 100), 0, 200, 2);


# .--------------.
# |  VUE / VIEWS |
# .--------------.

-- CLIENT INFORMATION
CREATE VIEW ClientsInformations AS
SELECT Clients.clientID, createurID, mail, motDePasse, prenomClient, nomClient, telephoneClient, adresseClient, soldeBanque, soldePoint, estAdmin
FROM Clients 
LEFT JOIN Createurs ON Clients.clientID = Createurs.clientID;

-- [AVANCEES] CREATEUR INFORMATION
CREATE VIEW CreateursInformations AS
SELECT 
	Createurs.createurID,
    Createurs.soldePoint,
    Clients.*,
    COUNT(Recettes.recetteID) as nbRecettes,
    COUNT(HistoriqueCommandes.recetteID) as nbRecettesCreeCommandes
FROM Createurs
INNER JOIN Clients ON Clients.clientID = Createurs.clientID
LEFT JOIN Recettes ON Createurs.createurID = Recettes.createurID
LEFT JOIN HistoriqueCommandes ON HistoriqueCommandes.recetteID = Recettes.recetteID
GROUP BY Createurs.createurID;

-- PRIX RECETTE
CREATE VIEW PrixRecettes AS
SELECT Recettes.recetteID, SUM(prixIngredient) as prix FROM Recettes
INNER JOIN IngredientRecette ON Recettes.recetteID = IngredientRecette.recetteID
INNER JOIN Ingredient ON Ingredient.ingredientID = IngredientRecette.ingredientID
GROUP BY Recettes.recetteID;
            
-- Ingredients avec categorie
CREATE VIEW ListeIngredientsView AS
SELECT Ingredient.*, IngredientCategorie.nomCategorie FROM Ingredient
INNER JOIN IngredientCategorie ON Ingredient.categorieID = IngredientCategorie.categorieID;

-- Ingredient avec categorie et nb de recettes lié
CREATE VIEW ListeIngredientsRecetteView AS
SELECT Ingredient.*, IngredientCategorie.nomCategorie, count(Recettes.recetteID) as nbRecettes FROM Ingredient
INNER JOIN IngredientCategorie ON Ingredient.categorieID = IngredientCategorie.categorieID
LEFT JOIN IngredientRecette ON IngredientRecette.ingredientID = Ingredient.ingredientID
LEFT JOIN Recettes ON IngredientRecette.recetteID = Recettes.recetteID
GROUP BY Ingredient.ingredientID
ORDER BY Ingredient.stock;

-- RECETTE VIEW : nom recettes, ingredients, prix.. etc
CREATE VIEW RecettesView AS
SELECT Recettes.*, 
	GROUP_CONCAT(DISTINCT Ingredient.nomIngredient SEPARATOR ", ") AS ingredients,
	SUM(Ingredient.prixIngredient) as prix
FROM Recettes
INNER JOIN IngredientRecette ON Recettes.recetteID = IngredientRecette.recetteID
INNER JOIN Ingredient ON Ingredient.ingredientID = IngredientRecette.ingredientID
GROUP BY Recettes.recetteID;

-- [AVANCEE|CREATEUR] RECETTE VIEW : detail + enrichies par statistique de consommation (nbCommande)
CREATE VIEW RecettesCreateurView AS
SELECT Recettes.*, 
	GROUP_CONCAT(DISTINCT Ingredient.nomIngredient SEPARATOR ", ") AS ingredients,
    (
		SELECT prix FROM PrixRecettes WHERE PrixRecettes.recetteID = Recettes.recetteID
	) as prix,
    COUNT(DISTINCT HistoriqueCommandes.commandeID) as nbCommande
FROM Recettes
INNER JOIN IngredientRecette ON Recettes.recetteID = IngredientRecette.recetteID
INNER JOIN Ingredient ON Ingredient.ingredientID = IngredientRecette.ingredientID
LEFT JOIN HistoriqueCommandes ON Recettes.recetteID = HistoriqueCommandes.recetteID
GROUP BY Recettes.recetteID
ORDER BY nbCommande DESC;

-- === VIEW RECENT ===
-- [COMMANDE | STOCK > 0] RECETTE VIEW : nom recettes, ingredients, prix.. etc (AVEC STOCK necessaire)
CREATE VIEW RecettesCommandeViewRecent AS
SELECT Recettes.*, 
GROUP_CONCAT(DISTINCT Ingredient.nomIngredient SEPARATOR ", ") AS ingredients, 
CAST(SUM(Ingredient.prixIngredient) AS DECIMAL) as prix -- le champ prix est enregistree comme decimal sur C#
FROM Recettes
INNER JOIN IngredientRecette ON Recettes.recetteID = IngredientRecette.recetteID
INNER JOIN Ingredient ON Ingredient.ingredientID = IngredientRecette.ingredientID
WHERE Ingredient.stock > 0
GROUP BY Recettes.recetteID
ORDER BY Recettes.dateRecette DESC;

-- === VIEW LES + POPULAIRE ===
-- affiche la liste des recettes (informations completes) les plus populaire via l'historique des commande

CREATE VIEW RecettesCommandeViewPopulaire AS
SELECT Recettes.*, 
       GROUP_CONCAT(DISTINCT Ingredient.nomIngredient SEPARATOR ", ") AS ingredients, 
       (
			SELECT prix FROM PrixRecettes WHERE PrixRecettes.recetteID = Recettes.recetteID
       ) as prix,
       COUNT(DISTINCT HistoriqueCommandes.commandeID) as nbCommandes
FROM Recettes
INNER JOIN IngredientRecette ON Recettes.recetteID = IngredientRecette.recetteID
INNER JOIN Ingredient ON Ingredient.ingredientID = IngredientRecette.ingredientID
LEFT JOIN HistoriqueCommandes ON Recettes.recetteID = HistoriqueCommandes.recetteID
WHERE Ingredient.stock > 0
GROUP BY Recettes.recetteID
ORDER BY nbCommandes DESC;

-- === VIEW LES + POPULAIRE << DE LA SEMAINE >> ===
-- affiche la liste des recettes les plus populaire via l'historique des commandes de la semaine
CREATE VIEW RecettesCommandeViewTopSemaine AS
SELECT Recettes.*, 
       GROUP_CONCAT(DISTINCT Ingredient.nomIngredient SEPARATOR ", ") AS ingredients, 
       (
			SELECT prix FROM PrixRecettes WHERE PrixRecettes.recetteID = Recettes.recetteID
       ) as prix,
       (
			SELECT count(*) FROM HistoriqueCommandes AS HC
				WHERE HC.recetteID = Recettes.recetteID 
                AND HC.dateCommande >= DATE_SUB(NOW(), INTERVAL 1 WEEK)
       ) as nbCommandes
FROM Recettes
INNER JOIN IngredientRecette ON Recettes.recetteID = IngredientRecette.recetteID
INNER JOIN Ingredient ON Ingredient.ingredientID = IngredientRecette.ingredientID
LEFT JOIN HistoriqueCommandes ON Recettes.recetteID = HistoriqueCommandes.recetteID
WHERE Ingredient.stock > 0
GROUP BY Recettes.recetteID
ORDER BY nbCommandes DESC;

# ============ VIEW TOP ============

# VUE TOP DES RECETTES LES PLUS COMMANDEE
-- RecettesCommandeViewTopSemaine

# Client de la semaine
-- celui qui a depensé le plus d'argent
CREATE VIEW TopClientSemaineView AS
SELECT Clients.*, SUM(HistoriqueCommandes.prix) AS montantDepense FROM Clients
INNER JOIN HistoriqueCommandes ON HistoriqueCommandes.clientID = Clients.clientID
WHERE HistoriqueCommandes.dateCommande >= DATE_SUB(NOW(), INTERVAL 1 WEEK)
GROUP BY Clients.clientID
ORDER BY montantDepense DESC;

# cdr de la semaine
-- celui qui a fait le plus de profit / rapporter le +
CREATE VIEW TopCreateurSemaineView AS
SELECT Createurs.createurID, Clients.*, sum(HistoriqueCommandes.prix) AS beneficeSemaine FROM Createurs
INNER JOIN Recettes ON Createurs.createurID = Recettes.createurID
INNER JOIN HistoriqueCommandes ON HistoriqueCommandes.recetteID = Recettes.recetteID
INNER JOIN Clients ON Createurs.clientID = Clients.clientID
GROUP BY Createurs.createurID
ORDER BY beneficeSemaine DESC;



-- commande des recette createurs
CREATE VIEW CommandesRecettesCreateursView AS
SELECT Recettes.createurID, Recettes.nomRecette, descriptif, prix, 
   CAST(prix*0.1*100 AS SIGNED) as gainPoints,
   dateCommande 
FROM HistoriqueCommandes
INNER JOIN Recettes ON HistoriqueCommandes.recetteID = Recettes.recetteID
ORDER BY dateCommande DESC;
   
DELIMITER $$;
# CREATION TRIGGERS
CREATE TRIGGER hash_password_trigger BEFORE INSERT ON Clients
FOR EACH ROW
BEGIN
   SET NEW.motDePasse = SHA2(NEW.motDePasse, 256);
END;

-- Mise a jour Solde Client apres commande
CREATE TRIGGER changer_solde_client BEFORE INSERT ON HistoriqueCommandes
FOR EACH ROW
BEGIN
	-- mise a jour solde client
	UPDATE Clients SET soldeBanque = soldeBanque - NEW.prix WHERE clientID = NEW.clientID;
END;

-- Mise a jour solde Createur apres commande
CREATE TRIGGER update_solde_createur AFTER INSERT ON HistoriqueCommandes
FOR EACH ROW
BEGIN
	DECLARE creaId INT;
    DECLARE gainPoints INT;
    
    SELECT createurID INTO @creaId FROM Recettes WHERE recetteID = NEW.recetteID;
	
    -- taux gain * convertionEnPointCreateur
    SET gainPoints = (NEW.prix*0.1)*100;
    
	-- mise a jour solde createur
	UPDATE Createurs
    SET soldePoint = soldePoint + gainPoints 
    WHERE createurID = @creaId;
END;

-- Mettre a jour le stock des ingredient suite a la commande
CREATE TRIGGER update_stock_ingredients AFTER INSERT ON HistoriqueCommandes
FOR EACH ROW
BEGIN
	-- mise a jour solde createur
	UPDATE Ingredient
    INNER JOIN IngredientRecette ON IngredientRecette.ingredientID = Ingredient.ingredientID
    INNER JOIN Recettes ON IngredientRecette.recetteID = Recettes.recetteID
    SET stock = stock - 1
    WHERE Recettes.recetteID = NEW.recetteID;
END;

# CREATION FUNCTIONS

-- CLIENTS
CREATE PROCEDURE `ObtenirClient` (email VARCHAR(100), mdp VARCHAR(255))
#DETERMINISTIC
BEGIN
   SELECT *
   FROM ClientsInformations
   WHERE mail = email AND motDePasse = SHA2(mdp, 256);
END;

CREATE PROCEDURE `CreeClient` (email VARCHAR(100), mdp VARCHAR(255), nom VARCHAR(30), prenom VARCHAR(30), telephone DECIMAL(10,0), addr VARCHAR(100))
#DETERMINISTIC
BEGIN
	DECLARE permissionsAdmin BOOL;
    
	IF (SELECT count(*) FROM Clients) = 0 THEN
		SET @permissionsAdmin = TRUE;
	ELSE 
		SET @permissionsAdmin = FALSE;
    END IF;
    
	INSERT INTO Clients (mail, motDePasse, nomClient, prenomClient, telephoneClient, adresseClient, estAdmin) VALUES (email, mdp, nom, prenom, telephone, addr, @permissionsAdmin);
	
    SELECT *
	FROM ClientsInformations
	WHERE clientID = LAST_INSERT_ID();
END;

CREATE PROCEDURE `ObtenirInformationsCreateur`(creaId INT) 
BEGIN
	SELECT *
    FROM CreateursInformations
    WHERE createurID = creaId;
END;

CREATE PROCEDURE `RechargerSoldeBancaire`(cliID INT) 
#DETERMINISTIC
BEGIN
	IF ((SELECT soldeBanque FROM Clients WHERE clientID = cliID) < 1000) THEN
		UPDATE Clients SET soldeBanque = 1000 WHERE clientID = cliID;
        SELECT * FROM ClientsInformations WHERE clientID = cliID;
	ELSE
		SELECT FALSE;
    END IF;
END;

-- CREATEUR
CREATE PROCEDURE `CreeProfilCreateur` (cliId INT)
#DETERMINISTIC
BEGIN
	IF ((SELECT count(*) FROM Createurs WHERE clientID = cliId) = 0) THEN
		INSERT INTO Createurs (clientID) VALUES (cliId);
	END IF;
    
	SELECT *
	FROM ClientsInformations
	WHERE ClientID = cliId;
END;

CREATE PROCEDURE `ObtenirRecettesCreateur` (creaId INT)
#DETERMINISTIC
BEGIN
   SELECT * FROM RecettesCreateurView WHERE createurID = creaId; 
END;

CREATE PROCEDURE `ConvertirSoldeCreateur` (creaID INT, montantAConvertir INT, OUT resultat BOOL)
#DETERMINISTIC
BEGIN
	DECLARE conversion, cliID, sPts INT DEFAULT 0;
    SET @conversion = montantAConvertir / 100;
    
    SELECT clientID, soldePoint INTO @cliID, @sPts FROM Createurs WHERE createurID = creaID;
	IF (@sPts >= montantAConvertir) THEN
		-- supprimer les points createur convertit
		UPDATE Createurs SET soldePoint = soldePoint - montantAConvertir WHERE createurID = creaID; 
        
        -- ajouter le montant convertit a la banque
        UPDATE Clients SET soldeBanque = soldeBanque + @conversion WHERE clientID = @cliID; 
        
        SET resultat = TRUE;
        -- retourner client
		SELECT * FROM ClientsInformations WHERE clientID = @cliID;
	ELSE 
		SET resultat = FALSE;
		SELECT FALSE;
    END IF;
END;

CREATE PROCEDURE `ObtenirStatsCreateur`(creaId INT, OUT nombreCommandes INT, OUT points INT)
BEGIN
	SELECT count(*) INTO nombreCommandes FROM CommandesRecettesCreateursView
	WHERE createurID = creaId;
    
    SELECT soldePoint INTO points FROM Createurs
    WHERE createurID = creaId;
END;

CREATE PROCEDURE `ObtenirListeCommandesRecettesCreateur` (creaId INT)
#DETERMINISTIC
BEGIN
   SELECT * FROM CommandesRecettesCreateursView
   WHERE createurID = creaId;
END;

-- RECETTES
CREATE PROCEDURE `CreeHeaderRecette` (creaID int, nom VARCHAR(30), descr VARCHAR(255))
BEGIN
   INSERT INTO Recettes (createurID, nomRecette, descriptif) VALUES (creaID, nom, descr);
   SELECT LAST_INSERT_ID();
END;


-- Liste de recettes pour un ingredient donnée
CREATE PROCEDURE `ListeRecettesParIngredient`(ingrID INT)
BEGIN
	SELECT RecettesView.* FROM RecettesView
	INNER JOIN IngredientRecette ON IngredientRecette.recetteID = RecettesView.recetteID
	WHERE IngredientRecette.ingredientID = ingrID;
END;

-- Produit
CREATE PROCEDURE `ListeIngredients`()
BEGIN
	SELECT * FROM ListeIngredientsView ORDER BY nomIngredient;
END;

CREATE PROCEDURE `AjouterIngredient`(nom VARCHAR(30), descr VARCHAR(255), catId INT, fourId INT, stockActuelle INT, stockMin INT, stockMax INT, prix INT)
BEGIN
	INSERT INTO Ingredient (fournisseurID, categorieID, nomIngredient, descriptif, prixIngredient, stock, stockMin, stockMax) VALUES 
    (
		fourId,
		catId,
        nom,
        descr,
        prix,
        stockActuelle,
        stockMin,
        stockMax
    );
    
    SELECT * FROM ListeIngredientsView WHERE ingredientID = LAST_INSERT_ID();
END;

CREATE PROCEDURE `Approvisionner`(ingID INT)
BEGIN
	UPDATE Ingredient SET stock = stockMax WHERE ingredientID = ingID;
    
    SELECT * FROM ListeIngredientsView WHERE ingredientID = ingID;
END;
    
-- COMMANDE
CREATE PROCEDURE `Commander` (
	cliID int, recID int, 
	utiliserPointCrea bool, 
	OUT succes BOOL,
    OUT prixSoldeCrea INT,
    OUT prixSoldeBanque INT,
    OUT idCommande INT
)
BEGIN
	DECLARE prixRecette INT;
    DECLARE soldeSuffisant BOOL DEFAULT TRUE;
    
    SET prixSoldeCrea = 0;
    SET prixSoldeBanque = 0;
    
	-- obtenir information de la recette a commander :
    SET @prixRecette = (
		SELECT prix FROM PrixRecettes WHERE PrixRecettes.recetteID = recID
    );
    
	-- si le client veut payer via son solde createur -> on convertit ses points
	IF (utiliserPointCrea = TRUE) THEN
		-- conversion
        -- si la conversion est impossible (verif interne de la proc) on bind le resultat dans soldeSuffisant
        SET prixSoldeCrea = @prixRecette*100;
		CALL ConvertirSoldeCreateur((SELECT createurID FROM Createurs WHERE clientID = cliID), prixSoldeCrea, @soldeSuffisant);
    ELSE
		SET prixSoldeBanque = @prixRecette;
		-- test solde suffisant pour payer commande
		IF (SELECT soldeBanque FROM Clients WHERE clientID = cliID) >= @prixRecette THEN 
			SET @soldeSuffisant = TRUE;
		ELSE
			SET @soldeSuffisant = FALSE;
		END IF;
    END IF;
    
    -- on effectue la req si le solde est suffisant
    IF (@soldeSuffisant = TRUE) THEN
		-- c'est partit on procede a la commande en ajouter une row dans HistoriqueCommandes
		INSERT INTO HistoriqueCommandes (recetteID, clientID, prix) VALUES (
			recID,
			cliID,
			@prixRecette
		);
        
		SET succes = TRUE;
        SET idCommande = LAST_INSERT_ID();
    ELSE 
		SET succes = FALSE;
    END IF;
END;

-- COMMANDES

CREATE PROCEDURE `ObtenirListeCommandes` (id INT)
#DETERMINISTIC
BEGIN
   SELECT Recettes.nomRecette, descriptif, prix, dateCommande FROM HistoriqueCommandes
   INNER JOIN Recettes ON HistoriqueCommandes.recetteID = Recettes.recetteID
   WHERE clientID = id
   ORDER BY dateCommande DESC;
END;

-- ADMIN
CREATE PROCEDURE `PromotionAdminClient` (cliID INT)
#DETERMINISTIC
BEGIN
	IF ((SELECT count(*) FROM Clients WHERE clientID = cliID AND estAdmin = FALSE) = 1) THEN
		UPDATE Clients SET estAdmin = TRUE WHERE clientID = cliID LIMIT 1;
        
        SELECT *
		FROM ClientsInformations
		WHERE ClientID = cliId;
	ELSE
		SELECT FALSE;
    END IF;
END;

CREATE PROCEDURE `SupprimerClient` (cliID INT)
#DETERMINISTIC
BEGIN
	SET FOREIGN_KEY_CHECKS=0; -- desactivation de la securité des foreign key
	DELETE FROM Clients WHERE clientID = cliID LIMIT 1;
    SET FOREIGN_KEY_CHECKS=1; -- reactivation
    
    SELECT TRUE;
    
END;

CREATE PROCEDURE `SupprimerRecette` (recID INT)
#DETERMINISTIC
BEGIN
	SET FOREIGN_KEY_CHECKS=0; -- desactivation de la securité des foreign key
	DELETE FROM Recettes WHERE recetteID = recID LIMIT 1;
    SET FOREIGN_KEY_CHECKS=1; -- reactivation
    
    SELECT TRUE;
END;
DELIMITER;