# .Net Package Description

>To complete: fill in the {placeholders}, remove the blockquote guidance

## Web Service to transform printable
### Transform printable  


## Description

* A .Net web service written in C#.
* Written for the Firmstep Transformation Team as a utility to be used to improve form design.
* **Business need:** Firmstep form printables lacked configurability. This web service produces nicer styled printables.
* **Functionality/interactions:** The web service itself is self contained, it takes input JSON and returns HTML. The XSLT is held within the web service.

## Technical description

* Language(s) used: C#
* Server(s) installed on: {server names and location}
* Source of code: Russ Casey, Awesome Consulting Ltd
* Pre-requisites: Windows Server with .Net and IIS

### What it doed and how it works
It takes Firmstep form JSON and if used a pre-submission integration on a stage it returns HTML into a hidden field which can then be used as a token for a printable (replacing the {unhiddensummary}/{visiblesummary} tokens).

## Key people

- Business owner: None.
- Product owner: Russ Casey
- Developer: Russ Casey
- Peer reviewer(s): Chris Dibben
- Test user(s): None
- Trained power user(s): Russ Casey

## Training an support plan

User training not needed. Ocorian form builders will be trained to use the new printable method.

## Component parts

- [x] Package Description (this document)
- [ ] Metrics
- [ ] Installation guide
- [ ] Usage guide
