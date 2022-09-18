import { DependencyContainer } from "tsyringe";
import { IPreAkiLoadMod } from "@spt-aki/models/external/IPreAkiLoadMod";
import { DynamicRouterModService } from "@spt-aki/services/mod/dynamicRouter/DynamicRouterModService";
import { HttpResponseUtil } from "@spt-aki/utils/HttpResponseUtil";

class CWX_MasterKey implements IPreAkiLoadMod
{
    
    private router: DynamicRouterModService;
    private cfg;
    private http: HttpResponseUtil;

    public preAkiLoad(container: DependencyContainer): void
    {
        this.router = container.resolve<DynamicRouterModService>("DynamicRouterModService");
        this.http = container.resolve<HttpResponseUtil>("HttpResponseUtil");
        this.cfg = require("./config.json");

        this.addRoute();
    }

    private addRoute() : void
    {
        this.router.registerDynamicRouter(
            "MasterKey",
            [
                {
                    url: "/cwx/masterkey",
                    action: (url, info, sessionId, output) =>
                    {
                        return this.onRequestConfig();
                    }
                }
            ],
            "MasterKey"
        )
    }

    private onRequestConfig(): any 
    {
        if (typeof this.cfg.keyId !== "string")
        {
            return this.http.noBody({ keyId: "5c1d0d6d86f7744bb2683e1f" });
        }
        
        return this.http.noBody({ keyId: this.cfg.keyId});
    }
}

module.exports = { mod: new CWX_MasterKey() }