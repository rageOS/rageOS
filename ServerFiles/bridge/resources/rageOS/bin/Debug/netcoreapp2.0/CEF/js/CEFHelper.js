class CefHelper {
    constructor(resourcePath) {
        this.path = resourcePath;
        this.open = false;
    }

    show() {
        if (this.open === false) {
            this.open = true;

            var resolution = API.getScreenResolution();

            this.browser = API.createCefBrowser(resolution.Width, resolution.Height, true);
            API.waitUntilCefBrowserInit(this.browser);
            API.setCefBrowserPosition(this.browser, 0, 0);
            API.loadPageCefBrowser(this.browser, this.path);
            API.showCursor(true);
            API.setCanOpenChat(false);
        }
    }

    showExternal() {
        if (this.open === false) {
            this.open = true;

            var resolution = API.getScreenResolution();

            this.browser = API.createCefBrowser(resolution.Width * 0.8, resolution.Height * 0.8, false);
            API.waitUntilCefBrowserInit(this.browser);
            API.setCefBrowserPosition(this.browser, resolution.Width * 0.1, resolution.Height * 0.1);
            API.loadPageCefBrowser(this.browser, this.path);
            API.showCursor(true);
            API.setCanOpenChat(false);
        }

    }

    destroy() {
        if (this.open === true) {
            this.open = false;
            API.destroyCefBrowser(this.browser);
			this.browser = null;
            API.showCursor(false);
            API.setCanOpenChat(true);
        }
    }

    eval(string) {
        this.browser.eval(string);
    }
}

var cef = null;

API.onResourceStart.connect(function () {
    cef = new CefHelper();
});

API.onResourceStop.connect(function () {
});

API.onServerEventTrigger.connect(function (event, args) {
    switch (event) {
        case "CEF_CREATE":
            if (cef !== null) {
                cef.destroy();
            }
            cef = new CefHelper("/CEF/" + args[0]);
            cef.show();
            break;
        case "CEF_CREATE_EXTERNAL":
            if (cef !== null) {
                cef.destroy();
            }
            cef = new CefHelper(args[0]);
            cef.showExternal();      
            break;
        case "CEF_DESTROY":
            if (cef !== null) {
                cef.destroy();
            }
            break;
        case "BankMoneyReceiver":
            ReceiveBankMoney(args[0]);
            break;
    }
});

//ATM
function depositMoney(money) {
    if (money !== null) {
        API.TriggerServerEvent("depositMoney", money);
    }
    closeCEF();
}

function withdrawMoney(money) {
    if (money !== null) {
        API.TriggerServerEvent("withdrawMoney", money);
    }
    closeCEF();
}

function GetBankMoney() {
    API.TriggerServerEvent("GetBankMoney");
}

function ReceiveBankMoney(money) {
    cef.browser.call("DisplayBankMoney", money);
}

function closeCEF() {
    cef.destroy();
}
