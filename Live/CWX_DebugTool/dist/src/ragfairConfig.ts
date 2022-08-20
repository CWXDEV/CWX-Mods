import { inject, injectable } from "tsyringe";

import { ConfigTypes } from "@spt-aki/models/enums/ConfigTypes";
import { ConfigServer } from "@spt-aki/servers/ConfigServer";
import { IRagfairConfig } from "@spt-aki/models/spt/config/IRagfairConfig";

import { CWX_ConfigHandler } from "./configHandler";
import { ragfairConfig } from "models/IConfig";

@injectable()
export class CWX_RagfairConfig
{
    private tables: IRagfairConfig;
    private config: ragfairConfig;

    constructor(
        @inject("ConfigServer") private configServer: ConfigServer,
        @inject("CWX_ConfigHandler") private configHandler: CWX_ConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.config = this.configHandler.getConfig().ragfairConfig;
        this.tables = this.configServer.getConfig(ConfigTypes.RAGFAIR);

        this.StaticTrader();
        this.RoublesOnly();
        this.DisableBSGBlacklist();
    }

    private StaticTrader(): void
    {
        if (this.config.staticTrader)
        {
            this.tables.traders["ragfair"] = true;
        }
    }

    private RoublesOnly(): void
    {
        if (this.config.roublesOnly)
        {
            this.tables.dynamic.currencies["5449016a4bdc2d6f028b456f"] = 100;
            this.tables.dynamic.currencies["5696686a4bdc2da3298b456a"] = 0;
            this.tables.dynamic.currencies["569668774bdc2da2298b4568"] = 0;
        }
    }

    private DisableBSGBlacklist(): void
    {
        if (this.config.disableBSGBlacklist)
        {
            this.tables.dynamic.blacklist.enableBsgList = false;
        }
    }
}