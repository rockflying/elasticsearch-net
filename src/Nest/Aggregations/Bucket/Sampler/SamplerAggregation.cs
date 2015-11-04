using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SamplerAggregation>))]
	public interface ISamplerAggregation : IBucketAggregation
	{
		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		[JsonProperty("field")]
		FieldName Field { get; set; }

		[JsonProperty("max_docs_per_value")]
		int? MaxDocsPerValue { get; set; }

		[JsonProperty("script")]
		Script Script { get; set; }

		[JsonProperty("execution_hint")]
		SamplerAggregationExecutionHint? ExecutionHint { get; set; }
	}

	public class SamplerAggregation : BucketAggregationBase, ISamplerAggregation
	{
		public SamplerAggregationExecutionHint? ExecutionHint { get; set; }
		public FieldName Field { get; set; }
		public int? MaxDocsPerValue { get; set; }
		public Script Script { get; set; }
		public int? ShardSize { get; set; }

		internal SamplerAggregation() { }

		public SamplerAggregation(string name) : base(name) { }
		
		internal override void WrapInContainer(AggregationContainer c) => c.Sampler = this;
	}

	public class SamplerAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<SamplerAggregationDescriptor<T>, ISamplerAggregation, T>, ISamplerAggregation
		where T : class
	{
		SamplerAggregationExecutionHint? ISamplerAggregation.ExecutionHint { get; set; }
		FieldName ISamplerAggregation.Field { get; set; }
		int? ISamplerAggregation.MaxDocsPerValue { get; set; }
		Script ISamplerAggregation.Script { get; set; }
		int? ISamplerAggregation.ShardSize { get; set; }

		public SamplerAggregationDescriptor<T> ExecutionHint(SamplerAggregationExecutionHint executionHint) =>
			Assign(a => a.ExecutionHint = executionHint);

		public SamplerAggregationDescriptor<T> Field(FieldName field) => Assign(a => a.Field = field);

		public SamplerAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public SamplerAggregationDescriptor<T> MaxDocsPerValue(int maxDocs) => Assign(a => a.MaxDocsPerValue = maxDocs);

		public SamplerAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = script);

		public SamplerAggregationDescriptor<T> Script(Func<ScriptDescriptor, Script> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public SamplerAggregationDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);
	}
}