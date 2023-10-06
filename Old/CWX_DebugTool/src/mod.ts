import { DependencyContainer, Lifecycle } from "tsyringe";
import { IPostDBLoadMod } from "@spt-aki/models/external/IPostDBLoadMod";

import { CwxLogging } from "./logging";
import { CwxConfigHandler } from "./configHandler";
import { CwxGlobalsConfig } from "./globalsConfig";
import { CwxRagfairConfig } from "./ragfairConfig";
import { CwxLocationConfig } from "./locationConfig";
import { CwxInraidConfig } from "./inraidConfig";
import { CwxItemsConfig } from "./itemsConfig";
import { CwxAirdropConfig } from "./airdropConfig";


class CWX_DebugTool implements IPostDBLoadMod
{
    public postDBLoad(container: DependencyContainer): void 
    {
        container.register<CwxConfigHandler>("CwxConfigHandler", CwxConfigHandler, {lifecycle:Lifecycle.Singleton})
        container.register<CwxGlobalsConfig>("CwxGlobalsConfig", CwxGlobalsConfig);
        container.register<CwxRagfairConfig>("CwxRagfairConfig", CwxRagfairConfig);
        container.register<CwxLocationConfig>("CwxLocationConfig", CwxLocationConfig);
        container.register<CwxInraidConfig>("CwxInraidConfig", CwxInraidConfig);
        container.register<CwxItemsConfig>("CwxItemsConfig", CwxItemsConfig);
        container.register<CwxAirdropConfig>("CwxAirdropConfig", CwxAirdropConfig);
        container.register<CwxLogging>("CwxLogging", CwxLogging);
        

        container.resolve<CwxGlobalsConfig>("CwxGlobalsConfig").applyChanges();
        container.resolve<CwxRagfairConfig>("CwxRagfairConfig").applyChanges();
        container.resolve<CwxLocationConfig>("CwxLocationConfig").applyChanges();
        container.resolve<CwxInraidConfig>("CwxInraidConfig").applyChanges();
        container.resolve<CwxItemsConfig>("CwxItemsConfig").applyChanges();
        container.resolve<CwxAirdropConfig>("CwxAirdropConfig").applyChanges();
        
        if (container.resolve<CwxConfigHandler>("CwxConfigHandler").getConfig().showLogs)
        {
            container.resolve<CwxLogging>("CwxLogging").sendLogging();
        }
    }
}

module.exports = { mod: new CWX_DebugTool() }