// Garden namespace
Type.registerNamespace("Garden");

// Tree class
Garden.Tree = function(name) {
	this._name = name;
};

Garden.Tree.prototype =
	{
		getName: function() {
			return this._name;
		},

		makeLeaves: function() {
		},

		toString: function() {
			return this._name;
		}
	};

Garden.Tree.registerClass("Garden.Tree");

// IFruitTree interface
Garden.IFruitTree = function() {
};

Garden.IFruitTree.prototype =
	{
		bearFruit: function() {
		}
	};

Garden.IFruitTree.registerInterface("Garden.IFruitTree");

// FruitTree class
Garden.FruitTree = function(name, description) {
	Garden.FruitTree.initializeBase(this, [name]);
	this._description = description;
};

Garden.FruitTree.prototype.bearFruit = function() {
	return this._description;
};

Garden.FruitTree.registerClass("Garden.FruitTree", Garden.Tree, Garden.IFruitTree);

// Apple
Garden.Apple = function() {
	Garden.Apple.initializeBase(this, ["Apple", "red and crunchy"]);
};

Garden.Apple.prototype =
	{
		makeLeaves: function() {
			alert("medium-sized and deciduous");
		},

		toString: function() {
			return "FruitTree " + Garden.Apple.callBaseMethod(this, "toString");
		}
	};

Garden.Apple.registerClass("Garden.Apple", Garden.FruitTree);

// Banana
Garden.Banana = function() {
	Garden.Banana.initializeBase(this, ["Banana", "yellow and squishy"]);
};

Garden.Banana.prototype =
	{
		makeLeaves: function() {
			alert("big and green");
		},

		toString: function() {
			return "FruitTree " + Garden.Banana.callBaseMethod(this, "toString");
		}
	};

Garden.Banana.registerClass("Garden.Banana", Garden.FruitTree);

// Pine
Garden.Pine = function() {
	Garden.Pine.initializeBase(this, ["Pine"]);
};

Garden.Pine.prototype.makeLeaves = function() {
	alert("Evergreen needles");
};

Garden.Pine.registerClass("Garden.Pine", Garden.Tree);

// Notify ScriptManager that this is the end of the script.
if (typeof(Sys) != 'undefined')
{
	Sys.Application.notifyScriptLoaded();
}
