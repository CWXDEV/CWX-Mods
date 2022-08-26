import { inject, injectable } from "tsyringe";

import { CWX_ConfigHandler } from "./configHandler";
import { DatabaseServer } from "@spt-aki/servers/DatabaseServer";
import { ITemplateItem } from "@spt-aki/models/eft/common/tables/ITemplateItem";
import { IConfig } from "models/IConfig";

@injectable()
export class CWX_ItemsConfig
{
    private tables: Record<string, ITemplateItem>;
    private config: IConfig;
    
    constructor(
        @inject("DatabaseServer") private databaseServer: DatabaseServer,
        @inject("CWX_ConfigHandler") private configHandler: CWX_ConfigHandler
    )
    {}

    public applyChanges(): void
    {
        this.tables = this.databaseServer.getTables().templates.items;
        this.config = this.configHandler.getConfig();

        this.changeShrapProps();
        this.changeMaxAmmoForKS23();
    }

    private changeShrapProps(): void
    {
        const shrap = this.tables["5e85a9a6eacf8c039e4e2ac1"];

        if (this.config.itemsConfig.changeShrapProps)
        {
            shrap._props.Damage = 200;
            shrap._props.InitialSpeed = 1000;
        }
    }

    private changeMaxAmmoForKS23(): void
    {
        const ks23 = this.tables["5f647d9f8499b57dc40ddb93"];

        if (this.config.itemsConfig.changeMaxAmmoForKS23)
        {
            ks23._props.Cartridges[0]._max_count = 30;
        }
    }
}