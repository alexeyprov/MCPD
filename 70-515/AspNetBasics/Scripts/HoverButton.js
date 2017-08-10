Type.registerNamespace("ControlExtensions");

ControlExtensions.HoverButton = function(element)
{
	ControlExtensions.HoverButton.initializeBase(this, [element]);

	this._clickDelegate = null;
	this._hoverDelegate = null;
	this._unhoverDelegate = null;
}

ControlExtensions.HoverButton.prototype =
{
	// Construction/Destruction
	initialize: function ()
	{
		ControlExtensions.HoverButton.callBaseMethod(this, "initialize");

		var element = this.get_element();

		// Create delegates
		if (null == this._clickDelegate)
		{
			this._clickDelegate = Function.createDelegate(this, this._clickHandler);
		}

		if (null == this._hoverDelegate)
		{
			this._hoverDelegate = Function.createDelegate(this, this._hoverHandler);
		}

		if (null == this._unhoverDelegate)
		{
			this._unhoverDelegate = Function.createDelegate(this, this._unhoverHandler);
		}

		// Link delegates to DOM events
		Sys.UI.DomEvent.addHandler(element, "click", this._clickDelegate);
		Sys.UI.DomEvent.addHandler(element, "mouseover", this._hoverDelegate);
		Sys.UI.DomEvent.addHandler(element, "focus", this._hoverDelegate);
		Sys.UI.DomEvent.addHandler(element, "mouseout", this._unhoverDelegate);
	},

	dispose: function ()
	{
		var element = this.get_element();

		// Unlink delegates from DOM events and delete them
		if (this._clickDelegate)
		{
			Sys.UI.DomEvent.removeHandler(element, "click", this._clickDelegate);
			delete this._clickDelegate;
		}

		if (this._hoverDelegate)
		{
			Sys.UI.DomEvent.removeHandler(element, "mouseover", this._hoverDelegate);
			Sys.UI.DomEvent.removeHandler(element, "focus", this._hoverDelegate);
			delete this._hoverDelegate;
		}

		if (this._unhoverDelegate)
		{
			Sys.UI.DomEvent.removeHandler(element, "mouseout", this._unhoverDelegate);
			delete this._unhoverDelegate;
		}

		ControlExtensions.HoverButton.callBaseMethod(this, "dispose");
	},

	// Properties
	get_text: function ()
	{
		//return this.get_element().innerHTML;
		return this.get_element().value;
	},

	set_text: function (text)
	{
		//this.get_element().innerHTML = text;
		this.get_element().value = text;
	},

	// Events
	add_click: function (handler)
	{
		this.get_events().addHandler("click", handler);
	},

	remove_click: function (handler)
	{
		this.get_events().removeHandler("click", handler);
	},

	add_hover: function (handler)
	{
		this.get_events().addHandler("hover", handler);
	},

	remove_hover: function (handler)
	{
		this.get_events().removeHandler("hover", handler);
	},

	add_unhover: function (handler)
	{
		this.get_events().addHandler("unhover", handler);
	},

	remove_unhover: function (handler)
	{
		this.get_events().removeHandler("unhover", handler);
	},

	// Implementation
	_clickHandler: function (event)
	{
		var h = this.get_events().getHandler("click");

		if (h)
		{
			h(this, Sys.EventArgs.Empty);
		}
	},

	_hoverHandler: function (event)
	{
		var h = this.get_events().getHandler("hover");

		if (h)
		{
			h(this, Sys.EventArgs.Empty);
		}
	},

	_unhoverHandler: function (event)
	{
		var h = this.get_events().getHandler("unhover");

		if (h)
		{
			h(this, Sys.EventArgs.Empty);
		}
	}
}

ControlExtensions.HoverButton.registerClass("ControlExtensions.HoverButton", Sys.UI.Control);

if (typeof(Sys) !== "undefined")
{
	Sys.Application.notifyScriptLoaded();
}