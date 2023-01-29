# Running parallel middleware pipelines in AspNet.Core

This write up documents updates the example given in the
reference below for AspNetCore 6.0

## Why would we want to more than one pipeline?

In some hosting scenarios, where you host both legacy and new parts 
of the application side by side, one might have serialization formats
that cannot break compatibility for the legacy side, while the new parts
of the application can.

You can also inject different implementations of the same interface
on the new application side. This is useful for versioning.

## Reference

https://www.strathweb.com/2017/04/running-multiple-independent-asp-net-core-pipelines-side-by-side-in-the-same-application/
