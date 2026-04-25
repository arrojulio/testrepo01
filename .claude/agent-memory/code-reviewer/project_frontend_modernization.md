---
name: Frontend modernization — jQuery/Bootstrap CDN upgrade
description: jQuery upgraded to 3.7.1 + Bootstrap 5.3.3 via jsDelivr CDN in both master pages; SRI hashes absent; intranet risk noted
type: project
---

jQuery was upgraded from 1.4.4 (local) to 3.7.1 (jsDelivr CDN) and Bootstrap 5.3.3 added via CDN in Site.Master and EmptyMasterPage.Master. jquery-migrate 3.4.1 added for Telerik 2011 compatibility.

**Why:** Frontend modernization initiative — correcting security vulnerabilities and outdated libraries.

**How to apply:** When reviewing further frontend changes, note that SRI hashes are still absent from all CDN tags, local fallbacks do not exist, and the intranet deployment risk has not been addressed. Also note DataTables 1.9.4 and FancyBox 1.3.4 are knowingly deferred and incompatible with jQuery 3.x.
