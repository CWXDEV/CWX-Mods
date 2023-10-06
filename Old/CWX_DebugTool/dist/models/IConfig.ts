export interface ICwxConfig
{
    showLogs: boolean
    globalsConfig: GlobalsConfig
    ragfairConfig: RagfairConfig
    locationConfig: LocationConfig
    inraidConfig: InraidConfig
    itemsConfig: ItemsConfig
    airdropConfig: AirdropConfig
}

export interface GlobalsConfig
{
    noFallDamage: boolean
    openFlea: boolean
    quickScav: boolean
}

export interface RagfairConfig
{
    staticTrader: boolean
    roublesOnly: boolean
    disableBSGBlacklist: boolean
}

export interface LocationConfig
{
    turnLootOff: boolean
}

export interface InraidConfig
{
    turnPVEOff: boolean
    extendRaidTimes: boolean
}

export interface ItemsConfig
{
    changeShrapProps: boolean
    changeMaxAmmoForKS23: boolean
    removeDevFromBlacklist: boolean
    inspectAllItems: boolean
}

export interface AirdropConfig
{
    enableAllTheTime: boolean
    changeFlightHeight: boolean
    changeStartTime: boolean
    changePlaneVolume: boolean
}