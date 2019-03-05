# FSTransformPrintable


## Description
A reusable package that adds the ability to generate good-looking PDF documents from Firmstep forms in a generic way. It includes good formatting for Select Many, Date, Map, Map Multiple and Subform fields; and can be easily styled using CSS. See the [example](example.pdf).

This solution works by using the `{xml_data}` token and an XSLT string in an HTTP POST integration. This transmits the data to a web application that applies the XSLT to the token, creating usable HTML markup. This is returned to the Firmstep solution to populate a text field called `response`. The `{response}` token is then used in a Printable integration.



## Installation guide
The package requires a .net web application to be installed on a web server - typically the Firmstep LIM. Two versions of this app are included - one using the new *.net Core*, and one that uses the older *.net Framework 4.5*. Despite using older technology, the Framework version is recommended as this can normally be installed on a Firmstep LIM server without needing to install any other components or reboot.

The installation process consists of:
1. Install the web application
2. Import the integrations and test form
3. Tune the printable CSS to suit your requirements 

### Install the web application

#### .net Framework (recommended)
1. Identify an appropriate IIS Web Server - typically the LIM server for your Firmstep system.
2. Create a new folder under `wwwroot` called `transformPrintable`
3. Copy the contents of `\code\dot net framework\install` into this location
4. In IIS manager, create a new application pool called 'transformPrintable', then update the folder to be an application using this pool.

#### .net core (if not using .net framework)

As above, with these additional steps:
- Download and install the "Server Hosting Installer" from [here](https://www.microsoft.com/net/download/dotnet-core/runtime-2.0.6)
- Do an iisreset from the command line or reboot (important)


### Import the integrations and test form
1. In your Firmstep system, under Admin>Integrations, import the `/json/Transform Printable IntegrationList.json` file. All of the integrations in this file will be placed in the `Transform Printable` category
2. Edit each of the imported integrations and set it to use the correct LIM. 
3. If you installed the web application in a location other than the LIM server, adjust the hostname in the `Transform Printable HTTP` integration's URL. If using .net core, set the URL to `http://localhost/transformPrintable/api/transform`
4. The TransformPrintable renders maps using Google maps, and needs an API key for this purpose. To configure this behaviour, you will need an [API Key](https://developers.google.com/maps/documentation/javascript/get-api-key). In Admin>Tokens, create a token called `gmaps_key`. For evaluation use, this Awesome Consulting key can be used: `AIzaSyDtTfcN54kfBVp73QBDb_eCKMvUvVqL3Es`. Please get your own free key for production systems.
5. Open the test form. Update the map multiple field to an appropriate lookup, or remove it.
6. Fill in the test form. Click the 'Run Transform' button - this should populate the `response` field with HTML code. If not, check the integration log for errors.
7. Click the 'Generate PDF' button on the test form - this should generate a PDF

### Tune the printable CSS to suit your requirements
1. Edit the `Transform Printable PDF` integration. 
2. Click the `<> Source` button on the rich editor to view the raw code.
3. Adjust the CSS as necessary. You can override the appearance of each field by referring to its' data name - e.g. #text1. In this way you can make a hidden field visible or a visible field hidden. There are examples of this in the PDF integration as supplied.

## Usage
Each form that uses the TransformPrintable will need:
- a hidden text area field called `response`
- a pre-submission integration to run the `Transform Printable HTTP` integration
- to use the `TransformPrintable PDF` integration to generate printable files where they are needed - e.g. as email attachments or in file integrations.

### Static text
The `{xml_data}` token that is used by the transformPrintable does not include any static fields from the form, so it is not possible for the PDF to include this content directly - however there is a work-around:
- Add a hidden text or textarea field to your form that has the static text as its default value. Set the caption to be `h1`, `h2`, `h3` or `para` - this will cause the content to be displayed like static text in the PDF. The data name of such fields is not important to the transformPrintable.
- You could then edit the static content field on the form, and replace the content text with a `{token}` reference to your hidden text or textarea field. This will mean that the electronic and PDF versions of the form will always have identical static content.

### Multi-stage Workflow
Consider a workflow process that has a large, complex first stage where an applicant fills in a lot of detail. Subsequent stages are simple, prompting only for approval information. The 'Case View' feature is used to allow approvers to see the original stage 1 submission. We want to create a PDF at the end of the process, showing all the details of the original application and the subsequent approvals.

There is a simple way to achieve this, by making these changes to the second stage:
- Add hidden text area field called `response2`, in addition to `response`
- Add a creation integration that calls the `Transform Printable backup response` integration. This will copy the content from `response` that was generated as stage 1 was submitted into the new `response2` field.
- Use the TransformPrintable as normal on stage 2. The PDF will include the content of {response2} prior to {response} - and hence the original application content will appear before the approval content.

Repeat the technique as required for more stages by adding fields called `response3` and `response4`.
