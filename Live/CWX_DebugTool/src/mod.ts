import { DependencyContainer, Lifecycle } from "tsyringe";
import { IPostDBLoadMod } from "@spt-aki/models/external/IPostDBLoadMod";

import { CWX_ConfigHandler } from "./configHandler";
import { CWX_GlobalsConfig } from "./globalsConfig";
import { CWX_Logging } from "./logging";
import { CWX_RagfairConfig } from "./ragfairConfig";
import { CWX_LocationConfig } from "./locationConfig";
import { CWX_InraidConfig } from "./inraidConfig";


class CWX_DebugTool implements IPostDBLoadMod
{
    public postDBLoad(container: DependencyContainer): void 
    {
        container.register<CWX_ConfigHandler>("CWX_ConfigHandler", CWX_ConfigHandler, {lifecycle:Lifecycle.Singleton})
        container.register<CWX_GlobalsConfig>("CWX_GlobalsConfig", CWX_GlobalsConfig);
        container.register<CWX_RagfairConfig>("CWX_RagfairConfig", CWX_RagfairConfig);
        container.register<CWX_LocationConfig>("CWX_LocationConfig", CWX_LocationConfig);
        container.register<CWX_InraidConfig>("CWX_InraidConfig", CWX_InraidConfig);
        container.register<CWX_Logging>("CWX_Logging", CWX_Logging);
        

        container.resolve<CWX_GlobalsConfig>("CWX_GlobalsConfig").applyChanges();
        container.resolve<CWX_RagfairConfig>("CWX_RagfairConfig").applyChanges();
        container.resolve<CWX_LocationConfig>("CWX_LocationConfig").applyChanges();
        container.resolve<CWX_InraidConfig>("CWX_InraidConfig").applyChanges();
        
        if (container.resolve<CWX_ConfigHandler>("CWX_ConfigHandler").getConfig().showLogs)
        {
            container.resolve<CWX_Logging>("CWX_Logging").SendLogging();
        }
    }
}

module.exports = { mod: new CWX_DebugTool() }