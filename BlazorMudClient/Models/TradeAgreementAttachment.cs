using System;
using System.Collections.Generic;

namespace BlossmAPI.Models;

public partial class TradeAgreementAttachment
{
    public string Filename { get; set; } = null!;

    public int? IdTradeAgreement { get; set; }

    public virtual TradeAgreement? IdTradeAgreementNavigation { get; set; }
}
