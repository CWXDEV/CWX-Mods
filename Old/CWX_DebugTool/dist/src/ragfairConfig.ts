import { inject, injectable } from "tsyringe";

import { ConfigTypes } from "@spt-aki/models/enums/ConfigTypes";
import { ConfigServer } from "@spt-aki/servers/ConfigServer";
import { IRagfairConfig } from "@spt-aki/models/spt/config/IRagfairConfig";

import { CwxConfigHandler } from "./configHandler";
import { RagfairConfig } from "models/IConfig";

@injectable()
export class CwxRagfairConfig
{
    private tables: IRagfairConfig;
    private config: RagfairConfig;

    constructor(
        @inject("ConfigServer") private configServer: ConfigServer,
        @inject("CwxConfigHandler") private configHandler: CwxConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.config = this.configHandler.getConfig().ragfairConfig;
        this.tables = this.configServer.getConfig(ConfigTypes.RAGFAIR);

        //this.staticTrader();
        this.roublesOnly();
        this.disableBSGBlacklist();
    }

    private staticTrader(): void
    {
        if (this.config.staticTrader)
        {
            this.tables.traders["ragfair"] = true;
        }
    }

    private roublesOnly(): void
    {
        if (this.config.roublesOnly)
        {
            this.tables.dynamic.currencies["5449016a4bdc2d6f028b456f"] = 100;
            this.tables.dynamic.currencies["5696686a4bdc2da3298b456a"] = 0;
            this.tables.dynamic.currencies["569668774bdc2da2298b4568"] = 0;
        }
    }

    private disableBSGBlacklist(): void
    {
        if (this.config.disableBSGBlacklist)
        {
            this.tables.dynamic.blacklist.enableBsgList = false;
        }
    }
}