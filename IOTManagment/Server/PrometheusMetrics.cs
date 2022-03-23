using System;
using Prometheus;

    public class PrometheusMetrics
    {
        public static readonly Counter TickTock = Metrics.CreateCounter("sampleapp_ticks_total", "Just keeps on ticking");
    }

