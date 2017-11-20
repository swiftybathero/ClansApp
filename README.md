# ClansApp - DEPRECATED

I'm no longer working on this project. There is a possibility that i will revive it in another form - preferably full-web MVC application.

# What it was supposed to be:

Simple Clash of Clans stats application - currently showing simple data grid of hardcoded Clan Tag.

### Development target:
Client-server desktop application + web application, that will allow users to look into various Clash of Clans data.

### How to get API Key:
1. Go to [Clash of Clans Developer website](https://developer.clashofclans.com/)
2. Register
3. Go to [My Account](https://developer.clashofclans.com/#/account)
4. [Create key](https://developer.clashofclans.com/#/new-key)
    * In the key creation form, you must fill **your IP** address in **ALLOWED IP ADDRESSES** section, otherwise you won't be able to access the API
5. Go to your newly created API Key and copy the **TOKEN**, paste it to **API Key** field on main application form

### TODO:

##### Code:
- [ ] Refactor ViewModels for Dependency Injection implementation
- [ ] Write extended Unit tests of View and ViewModels
- [ ] Refactor some parts of styles and templates, write addidional styles/templates for buttons, data grids, etc...
- [ ] Implement exception handling system, supporting file logging and providing exception info for ViewModel -> View

##### Features:
- [ ] Support for other Clan Tags,
- [ ] Additional features, e.g. detailed clan member info,

##### Application structure:
- [ ] Create data service, based on WCF SOAP structure or REST JSON,
- [ ] Modify client application in terms of consuming newly created service,
- [ ] Create ASP MVC web application